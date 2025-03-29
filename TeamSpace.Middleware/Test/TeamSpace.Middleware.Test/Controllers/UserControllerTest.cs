using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ExceptionExtensions;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Middleware.Controllers;

namespace TeamSpace.Middleware.Test.Controllers;
    
public class UserControllerTest
{
    private readonly IUserService _userService;
    private readonly UserController _userController;
    private readonly Fixture specimenBuilders;

    public UserControllerTest()
    {
        specimenBuilders = new Fixture();
        _userService = Substitute.For<IUserService>();
        _userController = new UserController(_userService);
    }

    [Fact]
    public async Task GetLoggedUser_WhenSuccessful_ReturnsOkWithUserData()
    {
        // Arrange
        var userGetResponse = specimenBuilders.Create<UserGetResponse>();
        _userService.GetLoggedUser().Returns(userGetResponse);

        // Act
        var result = await _userController.GetLoggedUser();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(userGetResponse);

        await _userService.Received(1).GetLoggedUser();
    }

    [Fact]
    public async Task GetUser_WhenUserExists_ReturnsOk()
    {
        // Arrange
        var userId = specimenBuilders.Create<Guid>();
        var userGetResponse = specimenBuilders.Create<UserGetResponse>();
        _userService.GetUser(userId).Returns(userGetResponse);

        // Act
        var result = await _userController.GetUser(userId);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(userGetResponse);
    }

    [Fact]
    public async Task PostUser_WhenUserIsCreated_ReturnsOk()
    {
        // Arrange
        var userPostRequest = specimenBuilders.Create<UserPostRequest>();
        var userPostResponse = specimenBuilders.Create<UserPostResponse>();
        _userService.CreateUser(userPostRequest).Returns(userPostResponse);

        // Act
        var result = await _userController.PostUser(userPostRequest);

        // Assert
        var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        okResult.Value.Should().BeAssignableTo<UserPostResponse>();
    }

    [Fact]
    public async Task PostUser_WhenUserAlreadyExists_ReturnsConflict()
    {
        // Arrange
        var userPostRequest = specimenBuilders.Create<UserPostRequest>();
        _userService.CreateUser(userPostRequest).Throws(x => throw new UserAlreadyExistsException(userPostRequest.Username));

        // Act
        var result = await _userController.PostUser(userPostRequest);

        // Assert
        var conflictResult = result.Should().BeOfType<ConflictObjectResult>().Subject;
    }

    [Fact]
    public async Task PostUser_WhenUserCreationFails_ReturnsBadRequest()
    {
        // Arrange
        var errorsList = new List<IdentityError> { new() { Code = "code", Description = "description" } };
        var userPostRequest = specimenBuilders.Create<UserPostRequest>();
        _userService.CreateUser(userPostRequest).Throws(x => throw new UserCreationException(errorsList));

        // Act
        var result = await _userController.PostUser(userPostRequest);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
    }

    [Fact]
    public async Task Login_WhenUserIsLoggedIn_ReturnsOk()
    {
        // Arrange
        var userLoginRequest = specimenBuilders.Create<UserLoginRequest>();
        var token = specimenBuilders.Create<string>();
        _userService.LoginUser(userLoginRequest).Returns(token);

        // Act
        var result = await _userController.Login(userLoginRequest);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().Be(token);
    }

    [Fact]
    public async Task Login_WhenUserIsUnauthorized_ReturnsUnauthorized()
    {
        // Arrange
        var userLoginRequest = specimenBuilders.Create<UserLoginRequest>();
        _userService.LoginUser(userLoginRequest).Throws(x => throw new UnauthorizedAccessException());

        // Act
        var result = await _userController.Login(userLoginRequest);

        // Assert
        var unauthorizedResult = result.Should().BeOfType<UnauthorizedObjectResult>().Subject;
    }
}