using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TeamSpace.Application.Services.Base;

namespace TeamSpace.Middleware.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpacesController(ISpaceService spaceService) : ControllerBase 
{
    private readonly ISpaceService _spaceService = spaceService;

    [HttpGet("{id}", Name = "GetSpacesByUserId")]
    public async Task<IActionResult> GetSpacesByUserId(Guid id)
    {
        try
        {
            var spaces = await _spaceService.GetSpacesByUserId(id);
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

}