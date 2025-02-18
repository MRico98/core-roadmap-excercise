using TeamSpace.Domain.Specifications.Base;

namespace TeamSpace.Domain.Specifications.User;

public sealed class UserByUsername : Specification<Entities.User>
{
    public UserByUsername(string username) : base(e => e.UserName == username)
    {
    }
}