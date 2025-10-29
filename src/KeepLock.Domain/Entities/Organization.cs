using KeepLock.Domain.Entities.Common;

namespace KeepLock.Domain.Entities;

public class Organization : BaseEntity
{
    public string Identifier { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public int Seats { get; set; } = 10;
    public int? MaxCollections { get; set; }
    public int? MaxStorageGb { get; set; }

    public bool IsUseGroups { get; set; } = false;
    public bool IsUse2fa { get; set; } = false;
    public bool IsEnabled { get; set; } = true;

    public Guid CreatedById { get; set; }
    public Guid? LastModifiedById { get; set; }

    public virtual User CreatedBy { get; set; } = null!;
    public virtual User? LastModifiedBy { get; set; }

    public virtual ICollection<OrganizationUser> Users { get; set; } = new List<OrganizationUser>();
    public virtual ICollection<OrganizationGroup> Groups { get; set; } = new List<OrganizationGroup>();
}