using KeepLock.Domain.Entities.Common;

namespace KeepLock.Domain.Entities;

public class OrganizationGroupUser : BaseEntity
{
    public Guid OrganizationGroupId { get; set; }
    public Guid OrganizationUserId { get; set; }
    public virtual OrganizationGroup OrganizationGroup { get; set; } = null!;
    public virtual OrganizationUser OrganizationUser { get; set; } = null!;
}