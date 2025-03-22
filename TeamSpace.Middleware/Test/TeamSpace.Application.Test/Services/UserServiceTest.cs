using TeamSpace.Application.Interfaces;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Application.Services;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TeamSpace.Domain.Exceptions;

namespace TeamSpace.Application.Test.Services;

public class UserServiceTest
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly Fixture _specimenBuilders;

    public UserServiceTest()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _userService = new UserService(_userRepository, _jwtTokenGenerator);
        _specimenBuilders = new Fixture();
    }

    [Fact]
    public async Task CreateUser_WhenUserIsCreated_ReturnsTrue()
    {
        foreach (var behavior in _specimenBuilders.Behaviors.OfType<ThrowingRecursionBehavior>().ToList())
        {
            _specimenBuilders.Behaviors.Remove(behavior);
        }

        _specimenBuilders.Behaviors.Add(new OmitOnRecursionBehavior());
        
        // Arrange
        var userPostRequest = _specimenBuilders.Create<UserPostRequest>();
        var user = _specimenBuilders.Create<User>();
        var identityResult = IdentityResult.Success;
        _userRepository.CreateUserAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(identityResult);

        // Act
        var result = await _userService.CreateUser(userPostRequest);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task CreateUser_WhenUserAlreadyExists_ThrowsUserAlreadyExistsException()
    {
        foreach (var behavior in _specimenBuilders.Behaviors.OfType<ThrowingRecursionBehavior>().ToList())
        {
            _specimenBuilders.Behaviors.Remove(behavior);
        }

        _specimenBuilders.Behaviors.Add(new OmitOnRecursionBehavior());
        // Arrange
        var userPostRequest = _specimenBuilders.Create<UserPostRequest>();
        var user = _specimenBuilders.Create<User>();
        _userRepository.GetUserByUsernameAsync(Arg.Any<string>()).Returns(user);

        // Act
        Func<Task> act = async () => await _userService.CreateUser(userPostRequest);

        // Assert
        await act.Should().ThrowAsync<UserAlreadyExistsException>();
    }
}