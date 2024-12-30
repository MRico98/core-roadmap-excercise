using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TeamSpace.Infraestructure.Context;
using TeamSpace.Domain.Entities;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Infraestructure.Repositories;
using TeamSpace.Application.Services.Base;
using TeamSpace.Application.Services;

namespace TeamSpace.Configuration;

public static class SeviceCollectionExtensions
{
    public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration of DbContext
        services.AddDbContext<TeamSpaceDbContext>();
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<TeamSpaceDbContext>()
            .AddDefaultTokenProviders();

        // Configuration of Jwt
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var jwtKey = configuration["Jwt:Key"];
            if (jwtKey == null)
            {
                throw new Exception("Jwt key is missing");
            }
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });

        return services;
    }
    
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<ISpaceService, SpaceService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}