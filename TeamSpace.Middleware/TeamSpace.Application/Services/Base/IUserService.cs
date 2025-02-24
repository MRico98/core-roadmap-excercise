using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Services.Base;
public interface IUserService
{
    Task<bool> CreateUser(UserPostRequest userPostRequest);
    Task<string> LoginUser(UserLoginRequest userLoginRequest);
    Task<UserGetResponse> GetUser(Guid id);
}
