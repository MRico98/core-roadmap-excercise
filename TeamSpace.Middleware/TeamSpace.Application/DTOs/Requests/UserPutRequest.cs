namespace TeamSpace.Application.DTOs.Requests
{
    public class UserPutRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
    }
}