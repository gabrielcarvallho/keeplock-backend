using KeepLock.Domain.Entities.Common;

namespace KeepLock.Domain.Entities;

public class Folder : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid? ParentFolderId { get; set; }
    public string FolderName { get; set; } = string.Empty;

    public virtual User User { get; set; } = null!;
    public virtual Folder? ParentFolder { get; set; }
    public virtual ICollection<Folder> SubFolders { get; set; } = new List<Folder>();
    public virtual ICollection<Cipher> Ciphers { get; set; } = new List<Cipher>();
}