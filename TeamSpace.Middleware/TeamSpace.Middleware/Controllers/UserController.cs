using Microsoft.AspNetCore.Mvc;
using TeamSpace.Application.Services.Base;
using TeamSpace.Application.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Middleware.Attributes;

namespace TeamSpace.Middleware.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetLoggedUser()
    {
        try
        {
            var user = await _userService.GetLoggedUser();

            return Ok(user);
        }
        catch (NotFoundByIdException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [StagingOnly]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _userService.GetUsers();

            return Ok(users);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    [StagingOnly]
    public async Task<IActionResult> GetUser(Guid id)
    {
        try
        {
            var user = await _userService.GetUser(id);
                    
            return Ok(user);
        }
        catch (NotFoundByIdException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> PostUser([FromBody] UserPostRequest registerUserDto)
    {
        try {
            var result = await _userService.CreateUser(registerUserDto);

            return CreatedAtAction(nameof(GetUser), new { id = result }, result);
        }
        catch (UserAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (UserCreationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest loginUserDto)
    {
        try 
        {
            var result = await _userService.LoginUser(loginUserDto);
        
            return Ok(result);        
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }
}