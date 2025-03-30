using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using TeamSpace.Application.Interfaces;
using TeamSpace.Application.Services;
using TeamSpace.Application.Services.Base;
using TeamSpace.Configuration;
using TeamSpace.Domain.Entities;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Infraestructure.Auth;
using TeamSpace.Infraestructure.Context;
using TeamSpace.Infraestructure.Repositories;

namespace TeamSpace.Integration.Test.Config;

public class MinimalServiceIntegrationTest
{
    public IServiceProvider ServiceProvider { get; }

    public MinimalServiceIntegrationTest()
    {
        var services = new ServiceCollection();

        services.AddDbContext<TeamSpaceDbContext>(options =>
            options.UseInMemoryDatabase("TestDb"),
            ServiceLifetime.Scoped);

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<TeamSpaceDbContext>()
            .AddDefaultTokenProviders();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Warning);
        });

        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<ISpaceService, SpaceService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "Jwt:Key", "test-key" },
                { "Jwt:Issuer", "test-issuer" },
                { "Jwt:Audience", "test-audience" }
            })
            .Build();

        services.AddSingleton<IConfiguration>(config);

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        ServiceProvider = services.BuildServiceProvider();

        using var scope = ServiceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<TeamSpaceDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        dbContext.Database.EnsureCreated();

        var testUser = new List<(User user, string password)>
        {
            (new User 
            { 
                UserName = "testuser1",
                Email = "test1@example.com",
                EmailConfirmed = true
            }, "TestPassword123!"),
            (new User 
            { 
                UserName = "testuser2",
                Email = "test2@example.com",
                EmailConfirmed = true
            }, "TestPassword123!")
        };

        foreach (var user in testUser)
        {
            userManager.CreateAsync(user.user, user.password).Wait();
        }

        dbContext.SaveChangesAsync().Wait();
    }
}
