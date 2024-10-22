using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TeamSpace.Domain.Entities.Base;

namespace TeamSpace.Domain.Entities;

public partial class User : IdentityUser<Guid>, IBaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid RoleId { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SpaceUserRelation> SpaceUserRelations { get; set; } = new List<SpaceUserRelation>();

    public virtual ICollection<Space> Spaces { get; set; } = new List<Space>();
}
