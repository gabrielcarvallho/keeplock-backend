using KeepLock.Domain.Entities;
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
    public DbSet<OrganizationGroup> OrganizationGroups { get; set; }
    public DbSet<OrganizationGroupUser> OrganizationGroupUsers { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Folder> Folders { get; set; }
    public DbSet<Cipher> Ciphers { get; set; }

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
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

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

            entity.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            entity.HasOne(e => e.LastModifiedBy)
                .WithMany()
                .HasForeignKey(e => e.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            entity.HasMany(e => e.Users)
                .WithOne(ou => ou.Organization)
                .HasForeignKey(o => o.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrganizationGroup> (entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.OrganizationId).IsRequired();

            entity.HasIndex(e => new { e.OrganizationId, e.Name }).IsUnique();

            entity.HasOne(e => e.Organization)
                .WithMany(o => o.Groups)
                .HasForeignKey(e => e.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        modelBuilder.Entity<OrganizationUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrganizationId).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.UserEmail).IsRequired().HasMaxLength(256);

            entity.Property(e => e.Status).IsRequired().HasConversion<int>();
            entity.Property(e => e.UserType).IsRequired().HasConversion<int>();

            entity.HasIndex(e => new { e.OrganizationId, e.UserId }).IsUnique();
            entity.HasIndex(e => new { e.OrganizationId, e.UserEmail }).IsUnique();

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

        modelBuilder.Entity<OrganizationGroupUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.OrganizationUserId);

            entity.HasOne(e => e.OrganizationGroup)
                .WithMany()
                .HasForeignKey(e => e.OrganizationGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.OrganizationUser)
                .WithMany()
                .HasForeignKey(e => e.OrganizationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Collection>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OwnerType).IsRequired().HasConversion<int>();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasIndex(e => e.ParentCollectionId);
            entity.HasIndex(e => new { e.OrganizationId, e.ParentCollectionId });
            entity.HasIndex(e => new { e.UserId, e.ParentCollectionId });

            entity.HasIndex(e => e.Name);
            entity.HasIndex(e => new { e.OrganizationId, e.Name });
            entity.HasIndex(e => new { e.UserId, e.Name });

            entity.HasOne(e => e.ParentCollection)
                .WithMany(c => c.ChildCollections)
                .HasForeignKey(e => e.ParentCollectionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Collections)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            entity.HasOne(e => e.Organization)
                .WithMany()
                .HasForeignKey(e => e.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            entity.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            entity.HasOne(e => e.LastModifiedBy)
                .WithMany()
                .HasForeignKey(e => e.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FolderName).IsRequired().HasMaxLength(100);

            entity.HasIndex(e => e.ParentFolderId);
            entity.HasIndex(e => new { e.UserId, e.ParentFolderId  });
            entity.HasIndex(e => new { e.UserId, e.FolderName }).IsUnique();

            entity.HasOne(e => e.ParentFolder)
                .WithMany(f => f.SubFolders)
                .HasForeignKey(e => e.ParentFolderId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Folders)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        modelBuilder.Entity<Cipher>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasConversion<int>();

            entity.HasIndex(e => e.CollectionId);
            entity.HasIndex(e => e.FolderId);
            entity.HasIndex(e => new { e.CollectionId, e.Name });
            entity.HasIndex(e => new { e.FolderId, e.Name });

            entity.HasOne(e => e.Collection)
                .WithMany(c => c.Ciphers)
                .HasForeignKey(e => e.CollectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            entity.HasOne(e => e.Folder)
                .WithMany(f => f.Ciphers)
                .HasForeignKey(e => e.FolderId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            entity.HasOne(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            entity.HasOne(e => e.LastModifiedBy)
                .WithMany()
                .HasForeignKey(e => e.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        });
    }
}