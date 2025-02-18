using System.Linq.Expressions;
using TeamSpace.Domain.Specifications.Base;

namespace TeamSpace.Domain.Specifications.Note;

public sealed class NoteBySpaceId : Specification<Entities.Note>
{
    public NoteBySpaceId(Guid spaceId) : base(e => e.SpaceId == spaceId)
    {
    }
}