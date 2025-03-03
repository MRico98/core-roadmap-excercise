using Microsoft.AspNetCore.Identity;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Domain.Repositories.Base
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<string> LoginUserAsync(string username, string password);
    }
}