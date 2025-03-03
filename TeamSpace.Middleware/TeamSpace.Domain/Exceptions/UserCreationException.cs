using Microsoft.AspNetCore.Identity;

namespace TeamSpace.Domain.Exceptions;

public class UserCreationException : Exception
{
    public UserCreationException(IEnumerable<IdentityError> errors)
        : base($"Error al crear usuario: {string.Join(", ", errors.Select(e => e.Description))}") 
    {
    }
}