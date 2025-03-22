using TeamSpace.Domain.Specifications.Base;

namespace TeamSpace.Domain.Specifications.User;

public sealed class UserByUsername(string username) : Specification<Entities.User>(e => e.UserName == username)
{
}