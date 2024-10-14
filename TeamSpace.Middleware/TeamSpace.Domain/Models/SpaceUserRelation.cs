using System;
using System.Collections.Generic;

namespace TeamSpace.Domain.Models;

public partial class SpaceUserRelation
{
    public Guid Id { get; set; }

    public Guid SpaceId { get; set; }

    public Guid UserId { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
