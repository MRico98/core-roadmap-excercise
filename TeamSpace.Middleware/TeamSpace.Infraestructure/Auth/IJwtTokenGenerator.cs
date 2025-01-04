namespace TeamSpace.Infraestructure.Auth
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(string userId, string email, string userName);
    }
}