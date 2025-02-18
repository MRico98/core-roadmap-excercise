namespace TeamSpace.Application.DTOs.Requests;

public class UserPostRequest
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PhoneNumber { get; set; }
    public Guid? RoleId { get; set; }
}