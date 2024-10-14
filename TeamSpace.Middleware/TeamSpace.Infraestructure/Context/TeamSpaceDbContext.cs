using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeamSpace.Infraestructure.Context;

public partial class TeamSpaceDbContext : DbContext
{
    public TeamSpaceDbContext()
    {
    }

    public TeamSpaceDbContext(DbContextOptions<TeamSpaceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Space> Spaces { get; set; }

    public virtual DbSet<SpaceUserRelation> SpaceUserRelations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=TeamSpace;User Id=sa;Password=password123!;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notes__3214EC077F0B2381");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Notes)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notes__CreatedBy__440B1D61");

            entity.HasOne(d => d.Space).WithMany(p => p.Notes)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notes__SpaceId__4316F928");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07FADE2666");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Space>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Spaces__3214EC0776D9232C");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Spaces)
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Spaces__Owner__3C69FB99");
        });

        modelBuilder.Entity<SpaceUserRelation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SpaceUse__3214EC073B278C96");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Space).WithMany(p => p.SpaceUserRelations)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SpaceUser__Space__3F466844");

            entity.HasOne(d => d.User).WithMany(p => p.SpaceUserRelations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SpaceUser__UserI__403A8C7D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0717A920A0");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
