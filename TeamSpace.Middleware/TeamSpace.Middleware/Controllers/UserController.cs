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

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userService.GetUser(id);

        if (user == null) return NotFound();
        
        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> PostUser([FromBody] UserPostRequest registerUserDto)
    {
        var result = await _userService.CreateUser(registerUserDto);

        if (result) return Ok(result);
        
        return BadRequest(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest loginUserDto)
    {
        var result = await _userService.LoginUser(loginUserDto);
        
        if (result != null) return Ok(result);
        
        return BadRequest(result);
    }
}