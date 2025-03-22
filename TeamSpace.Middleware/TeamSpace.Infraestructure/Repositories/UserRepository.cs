using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamSpace.Domain.Entities;
using TeamSpace.Infraestructure.Context;
using TeamSpace.Infraestructure.Auth;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Domain.Specifications.User;

namespace TeamSpace.Infraestructure.Repositories;

public class UserRepository(
    TeamSpaceDbContext dbContext,
    UserManager<User> userManager,
    SignInManager<User> signInManager) : Repository<User>(dbContext), IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;

    public async Task<SignInResult> SignInUserAsync(User user, string password) => await _signInManager.PasswordSignInAsync(user, password, false, false);

    public async Task<User?> GetUserByUsernameAsync(string username) => await _userManager.Users.Where(new UserByUsername(username).Criteria).FirstOrDefaultAsync();

    public async Task<IdentityResult> CreateUserAsync(User user, string password) => await _userManager.CreateAsync(user, password);
}