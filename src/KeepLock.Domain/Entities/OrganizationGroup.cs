using KeepLock.Domain.Entities.Common;

namespace KeepLock.Domain.Entities;

public class OrganizationGroup : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid OrganizationId { get; set; }
    public virtual Organization Organization { get; set; } = null!;
}