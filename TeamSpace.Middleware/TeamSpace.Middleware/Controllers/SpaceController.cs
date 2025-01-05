using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.Services.Base;

namespace TeamSpace.Middleware.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpacesController(ISpaceService spaceService) : ControllerBase 
{
    private readonly ISpaceService _spaceService = spaceService;

    [HttpGet]
    public async Task<IActionResult> GetSpacesByUserId([FromQuery] Guid userId)
    {
        try
        {
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

    public async Task<IActionResult> GetSpaces()
    {
        var spaces = await _spaceService.GetAllAsync();
        return Ok(spaces);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSpace(Guid id)
    {
        var space = await _spaceService.GetByIdAsync(id);
        if (space == null)
        {
            return NotFound();
        }
        return Ok(space);
    }

    [HttpPost]
    public async Task<IActionResult> PostSpace([FromBody] SpacePostRequest space)
    {
        var result = await _spaceService.CreateAsync(space);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

}