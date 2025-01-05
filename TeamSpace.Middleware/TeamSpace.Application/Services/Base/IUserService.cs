using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Services.Base;
public interface IUserService
{
    Task<bool> CreateUser(string username, string email, string password, string PhoneNumber,Guid roleId);
    Task<string> LoginUser(string username, string password);
    Task<List<UserGetResponse>> GetUsers();
    Task<UserGetResponse> GetUser(Guid id);
}
