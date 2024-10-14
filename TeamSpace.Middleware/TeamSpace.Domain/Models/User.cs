using System;
using System.Collections.Generic;

namespace TeamSpace.Domain.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid RoleId { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SpaceUserRelation> SpaceUserRelations { get; set; } = new List<SpaceUserRelation>();

    public virtual ICollection<Space> Spaces { get; set; } = new List<Space>();
}
