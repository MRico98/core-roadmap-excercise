using TeamSpace.Application.Interfaces;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Application.Services;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Application.DTOs.Responses;

using TeamSpace.Application.Test.Customizations;

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
        _specimenBuilders.Customize(new UserCustomization());
    }

    [Fact]
    public async Task GetLoggedUser_WhenSuccessful_ReturnsUserGetResponse()
    {
        // Arrange
        var user = _specimenBuilders.Create<User>();
        _userRepository.GetUserByEmailAsync(Arg.Any<string>()).Returns(user);

        // Act
        var result = await _userService.GetLoggedUser();

        // Assert
        result.Should().BeOfType<UserGetResponse>();
        result.Id.Should().Be(user.Id);
    }

    [Fact]
    public async Task GetLoggedUser_WhenUserDoesNotExist_ThrowsNotFoundByIdException()
    {
        // Arrange
        _userRepository.GetUserByEmailAsync(Arg.Any<string>()).Returns((User)null);

        // Act
        Func<Task> act = async () => await _userService.GetLoggedUser();

        // Assert
        await act.Should().ThrowAsync<NotFoundByIdException>();
    }

    [Fact]
    public async Task GetUsers_WhenUsersExist_ReturnsUserGetResponseList()
    {
        // Arrange
        var users = _specimenBuilders.CreateMany<User>().ToList();
        _userRepository.ListAllAsync().Returns(users);

        // Act
        var result = await _userService.GetUsers();

        // Assert
        result.Should().BeAssignableTo<IEnumerable<UserGetResponse>>();
        result.Count().Should().Be(users.Count);
    }

    [Fact]
    public async Task LoginUser_WhenUserExists_ReturnsJwtToken()
    {
        // Arrange
        var user = _specimenBuilders.Create<User>();
        _userRepository.GetUserByUsernameAsync(Arg.Any<string>()).Returns(user);
        _userRepository.SignInUserAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(SignInResult.Success);
        var jwtToken = _specimenBuilders.Create<string>();
        _jwtTokenGenerator.GenerateJwtToken(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(jwtToken);

        // Act
        var result = await _userService.LoginUser(_specimenBuilders.Create<UserLoginRequest>());

        // Assert
        result.Should().Be(jwtToken);
    }

    [Fact]
    public async Task LoginUser_WhenUserDoesNotExist_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        _userRepository.GetUserByUsernameAsync(Arg.Any<string>()).Returns((User)null);

        // Act
        Func<Task> act = async () => await _userService.LoginUser(_specimenBuilders.Create<UserLoginRequest>());

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task LoginUser_WhenPasswordIsInvalid_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var user = _specimenBuilders.Create<User>();
        _userRepository.GetUserByUsernameAsync(Arg.Any<string>()).Returns(user);
        _userRepository.SignInUserAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(SignInResult.Failed);

        // Act
        Func<Task> act = async () => await _userService.LoginUser(_specimenBuilders.Create<UserLoginRequest>());

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task CreateUser_WhenUserIsCreated_ReturnsTrue()
    {
        // Arrange
        var userPostRequest = _specimenBuilders.Create<UserPostRequest>();
        var user = _specimenBuilders.Create<User>();
        var identityResult = IdentityResult.Success;
        _userRepository.CreateUserAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(identityResult);

        // Act
        var result = await _userService.CreateUser(userPostRequest);

        // Assert
        result.Should().BeOfType<UserPostResponse>();
    }

    [Fact]
    public async Task CreateUser_WhenUserAlreadyExists_ThrowsUserAlreadyExistsException()
    {
        // Arrange
        var userPostRequest = _specimenBuilders.Create<UserPostRequest>();
        var user = _specimenBuilders.Create<User>();
        _userRepository.GetUserByUsernameAsync(Arg.Any<string>()).Returns(user);

        // Act
        Func<Task> act = async () => await _userService.CreateUser(userPostRequest);

        // Assert
        await act.Should().ThrowAsync<UserAlreadyExistsException>();
    }

    [Fact]
    public async Task CreateUser_WhenUserCreationFails_ThrowsUserCreationException()
    {
        // Arrange
        var userPostRequest = _specimenBuilders.Create<UserPostRequest>();
        var user = _specimenBuilders.Create<User>();
        var identityResult = IdentityResult.Failed(new IdentityError { Code = "code", Description = "description" });
        _userRepository.CreateUserAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(identityResult);

        // Act
        Func<Task> act = async () => await _userService.CreateUser(userPostRequest);

        // Assert
        await act.Should().ThrowAsync<UserCreationException>();
    }
}