using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TeamSpace.Domain.Entities.Base;

namespace TeamSpace.Domain.Entities;

public partial class Role : IdentityRole<Guid>, IBaseEntity
{
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
