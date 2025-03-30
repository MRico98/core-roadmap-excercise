using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Application.Services.Base;
using TeamSpace.Infraestructure.Context;
using TeamSpace.Integration.Test.Config;

namespace TeamSpace.Integration.Test.Services;

public class UserServiceIntegrationTest(MinimalServiceIntegrationTest testFactory) : IClassFixture<MinimalServiceIntegrationTest>
{
    private readonly MinimalServiceIntegrationTest _testFactory = testFactory;

    [Fact]
    public async Task UserService_WhenSuccessful_CheckCorrectMethods()
    {
        // Arrange
        using var scope = _testFactory.ServiceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        // Get Users

        // Arrange
        var userListCount = scope.ServiceProvider.GetRequiredService<TeamSpaceDbContext>().Users.Count();

        // Act
        var usersServiceList = await userService.GetUsers();

        // Assert
        Assert.NotNull(usersServiceList);
        Assert.NotEmpty(usersServiceList);
        Assert.Equal(userListCount, usersServiceList.Count());
        foreach (var user in usersServiceList)
        {
            user.Should().NotBeNull();
            user.Should().BeOfType<UserGetResponse>();
        }

        // Create User
        
        // Arrange
        Fixture fixture = new();
        var userPostRequest = fixture.Create<UserPostRequest>();

        // Act
        var userCreated = await userService.CreateUser(userPostRequest);

        // Assert
        userCreated.Should().NotBeNull();
        userCreated.Should().BeOfType<UserPostResponse>();

        // Get User by Id

        // Arrange
        var userId = userCreated.Id;

        // Act
        var userById = await userService.GetUser(userId);

        // Assert
        userById.Should().NotBeNull();
        userById.Should().BeOfType<UserGetResponse>();
        userById.Id.Should().Be(userId);
    }
}