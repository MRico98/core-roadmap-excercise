namespace TeamSpace.Application.DTOs.Requests
{
    public class NotePutRequest
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
