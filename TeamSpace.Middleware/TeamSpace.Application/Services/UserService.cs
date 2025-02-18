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

    public async Task<bool> CreateUser(string username, string email, string password, string PhoneNumber, Guid? roleId)
    {
        var result = await _userRepository.CreateUserAsync(username, email, password, PhoneNumber, roleId);
        return true;
    }

    public async Task<List<UserGetResponse>> GetUsers()
    {
        var users = _userRepository.ListQueryable();
        return await users.Select(new UserToUserGetResponse().BuildExpression()).ToListAsync();
    }

    public Task<string> LoginUser(UserLoginRequest userLoginRequest)
    {
        var result = _userRepository.LoginUserAsync(userLoginRequest.Username, userLoginRequest.Password);
        return result;
    }

    public Task<string> LoginUser(string username, string password)
    {
        var result = _userRepository.LoginUserAsync(username, password);
        return result;
    }

    public async Task<UserGetResponse> GetUser(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

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
