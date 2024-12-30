using Microsoft.AspNetCore.Mvc;
using TeamSpace.Application.Services.Base;
using TeamSpace.Application.DTOs.Requests;

namespace TeamSpace.Middleware.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserPostRequest registerUserDto)
    {
        var result = await _userService.CreateUser(registerUserDto.Username, registerUserDto.Email, registerUserDto.Password, registerUserDto.PhoneNumber, registerUserDto.RoleId);
        if (result)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("login")]
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