using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TeamSpace.Application.DTOs;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Application.DTOs.Requests;

namespace TeamSpace.Middleware.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotesController(INoteService noteService) : ControllerBase
{
    private readonly INoteService _noteService = noteService;

    [HttpGet("{id}" , Name = "GetNoteById")]
    [Authorize]
    public async Task<IActionResult> GetNoteById(Guid id)
    {
        try
        {
            var note = await _noteService.GetByIdAsync(id);

            if (note == null) return NotFound();

            return Ok(note);
        }
        catch (SqlException ex)
        {
            return Problem(title: "Error related to the database", statusCode: 500, detail: ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }

    [HttpGet("space/{spaceId}")]
    [Authorize]
    public async Task<IActionResult> GetNotesBySpaceId(Guid spaceId)
    {
        try
        {
            var note = await _noteService.GetByIdAsync(spaceId);

            if (note == null) return NotFound();

            return Ok(note);
        }
        catch (SqlException ex)
        {
            return Problem(title: "Error related to the database", statusCode: 500, detail: ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateNote([FromBody] NotePostRequest noteDto)
    {
        try
        {
            var noteCreated = await _noteService.CreateAsync(noteDto);
            return CreatedAtRoute("GetNoteById", new { id = noteCreated.Id }, noteCreated);
        }
        catch (SqlException ex)
        {
            return Problem(title: "Error related to the database", statusCode: 500, detail: ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(Guid id)
    {
        await _noteService.DeleteAsync(id);
        return NoContent();
    }
}