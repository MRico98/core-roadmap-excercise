using Microsoft.AspNetCore.Mvc;
using TeamSpace.Application.DTOs;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Exceptions;

namespace TeamSpace.Middleware.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetNotes()
    {
        var notes = await _noteService.GetAllAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
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
        catch (Exception ex)
        {
            return Problem(title: "mmmm... This should not have happened", statusCode: 500, detail: ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateNote([FromBody] NoteDto noteDto)
    {
        var note = await _noteService.CreateAsync(noteDto);
        return CreatedAtRoute("GetNoteById", new { id = note.Id }, note);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(Guid id, [FromBody] NoteDto noteDto)
    {
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