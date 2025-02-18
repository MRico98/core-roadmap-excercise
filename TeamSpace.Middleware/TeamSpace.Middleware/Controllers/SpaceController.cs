using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.Services.Base;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TeamSpace.Middleware.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpacesController(ISpaceService spaceService) : ControllerBase 
{
    private readonly ISpaceService _spaceService = spaceService;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetSpacesByUserId([FromQuery] Guid userId)
    {
        try
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == null || currentUserId != userId.ToString()) return Unauthorized("You are not authorized to access this resource");
            
            var spaces = await _spaceService.GetSpacesByUserId(userId);
            
            return Ok(spaces);
        }
        catch(SqlException ex)
        {
            return Problem(title: "Error related to the database", statusCode: 500, detail: ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetSpace(Guid id)
    {
        try 
        {
            var space = await _spaceService.GetByIdAsync(id);

            var spaceOwner = space.Owner;
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != spaceOwner.ToString()) return Unauthorized("You are not authorized to access this resource");

            if (space == null) return NotFound();

            return Ok(space);
        }
        catch(SqlException ex)
        {
            return Problem(title: "Error related to the database", statusCode: 500, detail: ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> PostSpace([FromBody] SpacePostRequest space)
    {
        try 
        {
            var result = await _spaceService.CreateAsync(space);

            if (result == null) return BadRequest(result);

            return Ok(result);
        }
        catch(SqlException ex)
        {
            return Problem(title: "Error related to the database", statusCode: 500, detail: ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }

}