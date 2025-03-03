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
    SignInManager<User> signInManager,
    IJwtTokenGenerator jwtTokenGenerator) : Repository<User>(dbContext), IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<string> LoginUserAsync(string username, string password)
    {
        var user = await _userManager.Users.Where(new UserByUsername(username).Criteria).FirstOrDefaultAsync();

        if (user == null) throw new UnauthorizedAccessException("User not found");

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if (!result.Succeeded) throw new UnauthorizedAccessException("Invalid password");

        var token = _jwtTokenGenerator.GenerateJwtToken(user.Id.ToString(), user.Email!, user.UserName!);
        return token;
    }

    public async Task<User?> GetUserByUsernameAsync(string username) => await _userManager.Users.Where(new UserByUsername(username).Criteria).FirstOrDefaultAsync();

    public async Task<IdentityResult> CreateUserAsync(User user, string password) => await _userManager.CreateAsync(user, password);
}