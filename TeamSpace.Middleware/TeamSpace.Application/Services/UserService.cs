using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Application.Selectors;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.Selectos;

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
        var user = new UserPostRequestToUser().BuildExpression().Compile()(userPostRequest);
        var result = await _userRepository.CreateUserAsync(user);
        return true;
    }
}
