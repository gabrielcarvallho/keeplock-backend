using KeepLock.Shared.Primitives.Records;

namespace KeepLock.Shared.Primitives.Interfaces;

public interface IPagedResult<out TObject>
{
    IReadOnlyCollection<TObject> Items { get; }
    Page Page { get; }
}