using KeepLock.Domain.Enums.Organization;
using KeepLock.Domain.Models.Common;

namespace KeepLock.Domain.Models.Organization;

public class OrganizationUser : BaseEntity
{
    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;

    public OrganizationUserStatus Status { get; set; } = OrganizationUserStatus.Invited;
    public OrganizationUserType Type { get; set; } = OrganizationUserType.User;
    public bool IsAccessAll { get; set; } = false;

    public virtual Organization Organization { get; set; } = null!;
    public virtual User? User { get; set; }
}