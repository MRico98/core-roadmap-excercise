namespace TeamSpace.Application.DTOs;
public class SpaceDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<NoteDto>? Notes { get; set; }
}
