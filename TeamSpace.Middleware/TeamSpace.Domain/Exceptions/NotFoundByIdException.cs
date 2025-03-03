namespace TeamSpace.Domain.Exceptions;

public class NotFoundByIdException : Exception
{
    public NotFoundByIdException(object id)
        : base($"Entity with id {id} not found")
    {
    }
}
