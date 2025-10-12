using KeepLock.Domain.Models.Common;

namespace KeepLock.Domain.Models.Organizations;

public class Organization : BaseEntity
{
    public string Identifier { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public int Seats;
    public int? MaxCollections;
    public int? MaxStorageGb;

    public bool IsUseGroups { get; set; } = false;
    public bool IsUseDirectory { get; set; } = false;
    public bool IsUse2fa { get; set; } = false;
    public bool IsEnabled { get; set; } = true;

    public virtual ICollection<OrganizationUser> Users { get; set; } = new List<OrganizationUser>();
}