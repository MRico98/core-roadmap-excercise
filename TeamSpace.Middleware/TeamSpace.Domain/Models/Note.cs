using System;
using System.Collections.Generic;

namespace TeamSpace.Domain.Models;

public partial class Note
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public Guid SpaceId { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Space Space { get; set; } = null!;
}
