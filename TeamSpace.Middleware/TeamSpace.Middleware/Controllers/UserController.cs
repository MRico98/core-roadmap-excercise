using Microsoft.AspNetCore.Mvc;
using TeamSpace.Application.Services.Base;
using TeamSpace.Application.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;

namespace TeamSpace.Middleware.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsers();
        if (users == null)
        {
            return NotFound();
        }
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostUser([FromBody] UserPostRequest registerUserDto)
    {
        var result = await _userService.CreateUser(registerUserDto.Username, registerUserDto.Email, registerUserDto.Password, registerUserDto.PhoneNumber, registerUserDto.RoleId);
        if (result)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest loginUserDto)
    {
        var result = await _userService.LoginUser(loginUserDto.Username, loginUserDto.Password);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}