using TeamSpace.Domain.Entities.Base;
using TeamSpace.Domain.Specifications.Base;

namespace TeamSpace.Domain.Specifications;
public class ByIdSpecification<T> : Specification<T> where T : BaseEntity
{
    public ByIdSpecification(Guid id) : base(e => e.Id == id)
    {
    }
}