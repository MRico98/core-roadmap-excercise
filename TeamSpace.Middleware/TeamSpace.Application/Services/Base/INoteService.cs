using TeamSpace.Application.DTOs;

namespace TeamSpace.Application.Services.Base;
public interface INoteService
{
    Task<IEnumerable<NoteDto>> GetAllAsync();
    Task<NoteDto> GetByIdAsync(Guid id);
    Task<NoteDto> CreateAsync(NoteDto noteDto);
    Task<NoteDto> UpdateAsync(NoteDto noteDto);
    Task<bool> DeleteAsync(Guid id);    
}
