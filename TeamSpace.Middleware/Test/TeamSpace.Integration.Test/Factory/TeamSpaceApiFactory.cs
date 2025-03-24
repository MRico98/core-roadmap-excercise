using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TeamSpace.Configuration;
using TeamSpace.Domain.Entities;
using TeamSpace.Infraestructure.Context;

namespace TeamSpace.Integration.Test.Factory;

public class TeamSpaceApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(async services =>
        {
            var dbContextDescription = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<TeamSpaceDbContext>)
            );

            if (dbContextDescription != null)
            {
                services.Remove(dbContextDescription);
            }

            services.AddDbContext<TeamSpaceDbContext>(options =>
            {
                options.UseInMemoryDatabase("TeamSpaceDb");
            });

            services.AddApplicationServices();

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<TeamSpaceDbContext>();
            var userManager = scopedServices.GetRequiredService<UserManager<User>>();
            db.Database.EnsureCreated();

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

            await db.SaveChangesAsync();
        });
    }
}