namespace TeamSpace.Application.DTOs.Responses;

public class SpaceWithNoteDetailsGetResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid Owner { get; set; }
    public List<NoteDto> Notes { get; set; } = new();
}