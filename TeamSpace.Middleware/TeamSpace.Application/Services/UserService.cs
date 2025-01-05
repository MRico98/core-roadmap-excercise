using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Entities;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Domain.Repositories.Base;

namespace TeamSpace.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<bool> CreateUser(string username, string email, string password, string PhoneNumber, Guid roleId)
    {
        var result = await _userRepository.CreateUserAsync(username, email, password, PhoneNumber, roleId);
        return true;
    }

    public async Task<List<UserGetResponse>> GetUsers()
    {
        var users = await _userRepository.GetUserListAsync();
        return users.Select(u => new UserGetResponse
        {
            Id = u.Id,
            Username = u.UserName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            CreatedAt = u.CreatedAt,
        }).ToList();
    }

    public Task<string> LoginUser(string username, string password)
    {
        var result = _userRepository.LoginUserAsync(username, password);
        return result;
    }

    public async Task<UserGetResponse> GetUser(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundByIdException(id);
        }
        return new UserGetResponse
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CreatedAt = user.CreatedAt,
        };
    }
}
