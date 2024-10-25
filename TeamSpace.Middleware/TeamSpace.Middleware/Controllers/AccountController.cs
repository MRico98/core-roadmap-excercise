using Microsoft.AspNetCore.Mvc;
using TeamSpace.Application.Services.Base;
using TeamSpace.Middleware.DTOs.Requests;

namespace TeamSpace.Middleware.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService userService;

    public AccountController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var result = await userService.RegisterUserAsync(registerUserDto.Username, registerUserDto.Password, registerUserDto.Email);
        if (result)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
