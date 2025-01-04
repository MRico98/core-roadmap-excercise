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

namespace TeamSpace.Application.Services;

public class UserService(
    UserManager<User> userManager, 
    SignInManager<User> signInManager,
    IConfiguration _configuration) : IUserService
{
    private readonly UserManager<User> userManager = userManager;
    private readonly SignInManager<User> signInManager = signInManager;

    public async Task<List<UserGetResponse>> GetUsers()
    {
        var users = await userManager.Users.ToListAsync();
        return users.Select(u => new UserGetResponse
        {
            Id = u.Id,
            Username = u.UserName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            CreatedAt = u.CreatedAt,
        }).ToList();
    }

    public async Task<UserGetResponse> GetUser(Guid id)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;
        return new UserGetResponse
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CreatedAt = user.CreatedAt,
        };
    }

    public async Task<bool> CreateUser(string username, string email, string password, string PhoneNumber, Guid roleId)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = username,
            Email = email,
            PhoneNumber = PhoneNumber,
            CreatedAt = DateTime.Now,
            RoleId = roleId,
        };
        var result = await userManager.CreateAsync(user, password);
        return true;
    }


    public async Task<string> LoginUser(string username, string password)
    {
        var user = await userManager.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserName == username);

        if (user == null) throw new UnauthorizedAccessException();
        
        var result = await signInManager.PasswordSignInAsync(user, password, false, false);

        if (!result.Succeeded)throw new UnauthorizedAccessException();    
        
        var token = GenerateJwtToken(user);
        
        return token;
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
