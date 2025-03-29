using TeamSpace.Domain.Specifications.Base;

namespace TeamSpace.Domain.Specifications.User;

public sealed class UserByEmail(string email) : Specification<Entities.User>(e => e.Email == email)
{
}