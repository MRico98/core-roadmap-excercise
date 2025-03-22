using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Application.Selectors;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.Interfaces;

namespace TeamSpace.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<string> LoginUser(UserLoginRequest userLoginRequest)
    {
        var user = await _userRepository.GetUserByUsernameAsync(userLoginRequest.Username);

        if (user == null) throw new UnauthorizedAccessException("User not found");

        var signInResult = await _userRepository.SignInUserAsync(user, userLoginRequest.Password);
        
        if (!signInResult.Succeeded) throw new UnauthorizedAccessException("Invalid password");

        var result = jwtTokenGenerator.GenerateJwtToken(user.Id.ToString(), user.Email!, user.UserName!);

        return result;
    }

    public async Task<UserGetResponse> GetUser(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null) throw new NotFoundByIdException(id);

        return new UserToUserGetResponse().BuildExpression().Compile()(user);
    }

    public async Task<bool> CreateUser(UserPostRequest userPostRequest)
    {
        var userByUsername = await _userRepository.GetUserByUsernameAsync(userPostRequest.Username);

        if (userByUsername != null) throw new UserAlreadyExistsException(userPostRequest.Username);

        var user = new UserPostRequestToUser().BuildExpression().Compile()(userPostRequest);
        
        var creationResult = await _userRepository.CreateUserAsync(user, userPostRequest.Password);
        
        if (!creationResult.Succeeded) throw new UserCreationException(creationResult.Errors);

        return creationResult.Succeeded;
    }
}
