using TeamSpace.Domain.Specifications.Base;

namespace TeamSpace.Domain.Specifications.Space;
public class SpaceByOwnerId<T> : Specification<T> where T : Entities.Space
{
    public SpaceByOwnerId(Guid ownerId) : base(e => e.Owner == ownerId)
    {
    }
}
