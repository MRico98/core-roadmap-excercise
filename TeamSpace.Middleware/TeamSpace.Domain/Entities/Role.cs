using System;
using System.Collections.Generic;
using TeamSpace.Domain.Entities.Base;

namespace TeamSpace.Domain.Entities;

public partial class Role : IBaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
