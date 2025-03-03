using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Infraestructure.Context;

public partial class TeamSpaceDbContext : IdentityDbContext<User, Role, Guid>
{
    public TeamSpaceDbContext()
    {
    }

    public TeamSpaceDbContext(DbContextOptions<TeamSpaceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Space> Spaces { get; set; }

    public virtual DbSet<SpaceUserRelation> SpaceUserRelations { get; set; }
}
