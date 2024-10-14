using System;
using System.Collections.Generic;

namespace TeamSpace.Domain.Models;

public partial class Space
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid Owner { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual User OwnerNavigation { get; set; } = null!;

    public virtual ICollection<SpaceUserRelation> SpaceUserRelations { get; set; } = new List<SpaceUserRelation>();
}
