using TeamSpace.Application.DTOs;
using TeamSpace.Application.DTOs.Requests;

namespace TeamSpace.Application.Services.Base;
public interface INoteService
{
    Task<IEnumerable<NoteDto>> GetAllAsync();
    Task<NoteDto> GetByIdAsync(Guid id);
    Task<NoteDto> CreateAsync(NotePostRequest noteDto);
    Task<bool> DeleteAsync(Guid id);    
}
