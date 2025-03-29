using Microsoft.Extensions.DependencyInjection;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.Interfaces;
using TeamSpace.Application.Services;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Integration.Test.Factory;

namespace TeamSpace.Integration.Test.UserTest;

public class UserServiceIntegrationTests : IClassFixture<TeamSpaceApiFactory>
{
    private readonly TeamSpaceApiFactory _factory;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserServiceIntegrationTests(TeamSpaceApiFactory factory)
    {
        _factory = factory;
        var scope = _factory.Services.CreateScope();
        _userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        _jwtTokenGenerator = scope.ServiceProvider.GetRequiredService<IJwtTokenGenerator>();
        _userService = new UserService(_userRepository, _jwtTokenGenerator);
    }

    [Fact]
    public async Task GetUsers_WhenSuccessful_ReturnsListOfUsers()
    {
        // Arrange
        // Act
        var result = await _userService.GetUsers();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() == 2);
    }
}