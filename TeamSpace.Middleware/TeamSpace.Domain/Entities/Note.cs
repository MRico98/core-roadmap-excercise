using System;
using System.Collections.Generic;
using TeamSpace.Domain.Entities.Base;

namespace TeamSpace.Domain.Entities;

public partial class Note : BaseEntity
{
    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public Guid SpaceId { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Space Space { get; set; } = null!;
}
