using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSpace.Domain.Entities.Base;
using TeamSpace.Domain.Specifications.Base;

namespace TeamSpace.Domain.Specifications.Space;
public class SpaceByOwnerId<Space> : Specification<Space> where Space : BaseEntity
{
    public SpaceByOwnerId(Guid ownerId) : base(e => e.Owner == ownerId)
    {
    }
}
