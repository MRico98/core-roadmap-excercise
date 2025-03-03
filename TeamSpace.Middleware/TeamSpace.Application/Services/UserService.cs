using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Application.Selectors;
using TeamSpace.Application.DTOs.Requests;

namespace TeamSpace.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public Task<string> LoginUser(UserLoginRequest userLoginRequest)
    {
        var result = _userRepository.LoginUserAsync(userLoginRequest.Username, userLoginRequest.Password);
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

        return true;
    }
}
