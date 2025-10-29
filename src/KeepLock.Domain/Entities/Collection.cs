using KeepLock.Domain.Entities.Common;
using KeepLock.Domain.Enums;

namespace KeepLock.Domain.Entities;

public class Collection : BaseEntity
{
    public Guid? UserId { get; set; }
    public Guid? OrganizationId { get; set; }
    public Guid? ParentCollectionId { get; set; }
    public CollectionOwnerType OwnerType { get; set; } = CollectionOwnerType.User;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;

    public Guid? CreatedById { get; set; }
    public Guid? LastModifiedById { get; set; }

    public virtual User? CreatedBy { get; set; }
    public virtual User? LastModifiedBy { get; set; }
    public virtual User? User { get; set; }
    public virtual Organization? Organization { get; set; }
    public virtual Collection? ParentCollection { get; set; }
    public virtual ICollection<Collection> ChildCollections { get; set; } = new List<Collection>();
    public virtual ICollection<Cipher> Ciphers { get; set; } = new List<Cipher>();
}