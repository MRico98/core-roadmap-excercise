using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamSpace.Domain.Entities;
using TeamSpace.Infraestructure.Context;
using Microsoft.Extensions.Configuration;
using TeamSpace.Infraestructure.Auth;
using TeamSpace.Domain.Repositories.Base;

namespace TeamSpace.Infraestructure.Repositories;

public class UserRepository(
    TeamSpaceDbContext dbContext,
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IJwtTokenGenerator jwtTokenGenerator,
    IConfiguration configuration) : Repository<User>(dbContext), IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IConfiguration _configuration = configuration;

    public async Task<List<User>> GetUserListAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> CreateUserAsync(string username, string email, string password, string phoneNumber, Guid roleId)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = username,
            Email = email,
            PhoneNumber = phoneNumber,
            CreatedAt = DateTime.Now,
            RoleId = roleId,
        };
        var result = await _userManager.CreateAsync(user, password);
        return user;
    }

    public async Task<string> LoginUserAsync(string username, string password)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if (user == null) throw new UnauthorizedAccessException("User not found");

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if (!result.Succeeded) throw new UnauthorizedAccessException("Invalid password");

        var token = _jwtTokenGenerator.GenerateJwtToken(user.Id.ToString(), user.Email, user.UserName);
        return string.Empty;
    }
}