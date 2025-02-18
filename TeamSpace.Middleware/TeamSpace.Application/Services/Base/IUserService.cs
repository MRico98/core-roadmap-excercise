using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Services.Base;
public interface IUserService
{
    Task<bool> CreateUser(UserPostRequest userPostRequest);
    Task<bool> CreateUser(string username, string email, string password, string PhoneNumber, Guid? roleId);
    Task<string> LoginUser(string username, string password);
    Task<string> LoginUser(UserLoginRequest userLoginRequest);
    Task<List<UserGetResponse>> GetUsers();
    Task<UserGetResponse> GetUser(Guid id);
}
