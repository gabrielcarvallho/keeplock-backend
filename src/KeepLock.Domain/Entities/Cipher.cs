using KeepLock.Domain.Entities.Common;
using KeepLock.Domain.Enums;

namespace KeepLock.Domain.Entities;

public class Cipher : BaseEntity
{
    public Guid CollectionId { get; set; }
    public Guid? FolderId { get; set; }
    public ItemType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string EncryptedData { get; set; } = "{}";
    public bool IsFavorite { get; set; } = false;

    public Guid CreatedById { get; set; }
    public Guid? LastModifiedById { get; set; }

    public bool IsEnabled { get; set; } = true;

    public virtual Collection Collection { get; set; } = null!;
    public virtual Folder? Folder { get; set; }
    public virtual User CreatedBy { get; set; } = null!;
    public virtual User? LastModifiedBy { get; set; }
}