using TeamSpace.Domain.Entities;

namespace TeamSpace.Domain.Repositories.Base
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> CreateUserAsync(User user);
        Task<string> LoginUserAsync(string username, string password);
    }
}