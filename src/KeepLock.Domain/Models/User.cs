using KeepLock.Domain.Enums;
using KeepLock.Domain.Models.Common;
using KeepLock.Domain.Models.Organizations;

namespace KeepLock.Domain.Models;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public ProviderType AuthProvider { get; set; } = ProviderType.Email;
    public string? OAuthProviderId { get; set; }

    public string? MasterPasswordHash { get; set; }
    public string? MasterPasswordHint { get; set; }
    public bool IsMasterPasswordSet => !string.IsNullOrEmpty(MasterPasswordHash);

    public string? PublicKey { get; set; }
    public string? EncryptedPrivateKey { get; set; }

    public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
    public bool IsEmailVerified { get; set; } = false;

    public DateTime? LastPasswordChangeDate { get; set; }
    public bool IsEnabled { get; set; } = true;

    public virtual ICollection<OrganizationUser> Organizations { get; set; } = new List<OrganizationUser>();
}