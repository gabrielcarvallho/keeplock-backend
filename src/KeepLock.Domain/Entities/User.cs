using KeepLock.Domain.Entities.Common;
using KeepLock.Domain.Enums;

namespace KeepLock.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
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
    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
}