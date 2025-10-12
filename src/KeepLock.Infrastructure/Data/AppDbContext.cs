using KeepLock.Domain.Models;
using KeepLock.Domain.Models.Organizations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KeepLock.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationUser> OrganizationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        ConfigureEntities(modelBuilder);
    }

    private void ConfigureEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);

            entity.Property(e => e.AuthProvider).IsRequired().HasConversion<int>();
            entity.Property(e => e.OAuthProviderId).HasMaxLength(256);

            entity.Property(e => e.MasterPasswordHash).HasMaxLength(500);
            entity.Property(e => e.MasterPasswordHint).HasMaxLength(500);

            entity.Property(e => e.PublicKey).HasMaxLength(2048);
            entity.Property(e => e.EncryptedPrivateKey).HasMaxLength(4096);

            entity.Property(e => e.SecurityStamp).IsRequired().HasMaxLength(100);

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(u => new { u.AuthProvider, u.OAuthProviderId });

            entity.HasMany(e => e.Organizations)
                  .WithOne(o => o.User)
                  .HasForeignKey(o => o.UserId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Identifier).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);

            entity.Property(e => e.Seats).IsRequired();

            entity.HasIndex(e => e.Identifier).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Name);

            entity.HasMany(e => e.Users)
                .WithOne(ou => ou.Organization)
                .HasForeignKey(o => o.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrganizationUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrganizationId).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);

            entity.Property(e => e.Status).IsRequired().HasConversion<int>();
            entity.Property(e => e.Type).IsRequired().HasConversion<int>();

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }).IsUnique();
            entity.HasIndex(e => new { e.OrganizationId, e.Email }).IsUnique();

            entity.HasOne(e => e.Organization)
                .WithMany(o => o.Users)
                .HasForeignKey(e => e.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(u => u.Organizations)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        });
    }
}