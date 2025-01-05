namespace TeamSpace.Application.DTOs.Requests;

public class SpacePostRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid Owner { get; set; }
}