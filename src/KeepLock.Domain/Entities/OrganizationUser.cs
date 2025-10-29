using KeepLock.Domain.Entities.Common;
using KeepLock.Domain.Enums;

namespace KeepLock.Domain.Entities;

public class OrganizationUser : BaseEntity
{
    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public string UserEmail { get; set; } = string.Empty;

    public OrganizationUserStatus Status { get; set; } = OrganizationUserStatus.Invited;
    public OrganizationUserType UserType { get; set; } = OrganizationUserType.ReadOnly;
    public bool IsAccessAll { get; set; } = false;

    public virtual Organization Organization { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}