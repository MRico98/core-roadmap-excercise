using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TeamSpace.Application.DTOs;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Application.DTOs.Requests;

namespace TeamSpace.Middleware.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class NotesController(INoteService noteService) : ControllerBase
{
    private readonly INoteService _noteService = noteService;

    [HttpGet]
    public async Task<IActionResult> GetNotes()
    {
        try
        {
            var notes = await _noteService.GetAllAsync();
            return Ok(notes);
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

    [HttpGet("{id}", Name = "GetNoteById")]
    public async Task<IActionResult> GetNoteById(Guid id)
    {
        try
        {
            var note = await _noteService.GetByIdAsync(id);
            return Ok(note);
        }
        catch (NotFoundByIdException ex)
        {
            return NotFound(new { message = ex.Message });
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
            var note = new NoteDto
            {
                Title = noteDto.Title,
                Content = noteDto.Content
            };
            var noteCreated = await _noteService.CreateAsync(note);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote([FromBody] NotePutRequest noteRequest)
    {
        var noteDto = new NoteDto
        {
            Id = noteRequest.Id,
            Title = noteRequest.Title,
            Content = noteRequest.Content
        };
        var note = await _noteService.UpdateAsync(noteDto);
        return Ok(note);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(Guid id)
    {
        await _noteService.DeleteAsync(id);
        return NoContent();
    }
}