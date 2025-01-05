namespace TeamSpace.Application.DTOs.Responses;
public class SpaceGetResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid Owner { get; set; }
}