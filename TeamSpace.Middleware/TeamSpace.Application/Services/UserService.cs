using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Entities;
using TeamSpace.Domain.Repositories.Base;

namespace TeamSpace.Application.Services;
public class UserService : IUserService
{
    private readonly UserManager<User> userManager;

    public UserService(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<bool> RegisterUserAsync(string username, string password, string email)
    {
        var user = new User
        {
            UserName = username,
            Email = email,
            CreatedAt = DateTime.Now,
        };
        var result = await userManager.CreateAsync(user, password);
        return true;
    }
}
