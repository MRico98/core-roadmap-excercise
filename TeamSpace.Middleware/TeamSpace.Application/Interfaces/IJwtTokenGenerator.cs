namespace TeamSpace.Application.Interfaces;
public interface IJwtTokenGenerator
{
    string GenerateJwtToken(string userId, string email, string userName);
}