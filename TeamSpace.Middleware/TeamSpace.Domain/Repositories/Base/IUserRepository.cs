using TeamSpace.Domain.Entities;

namespace TeamSpace.Domain.Repositories.Base
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetUserListAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task<User> CreateUserAsync(string username, string email, string password, string phoneNumber, Guid? roleId);
        Task<string> LoginUserAsync(string username, string password);
    }
}