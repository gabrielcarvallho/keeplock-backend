using KeepLock.Shared.Primitives.Interfaces;
using KeepLock.Shared.Primitives.Records;
using System.Text.Json.Serialization;

namespace KeepLock.Shared.Results;

public record PagedResult<TItem>(IReadOnlyCollection<TItem> Items, Paging Paging)
        : IPagedResult<TItem> where TItem : class
{
    public Page Page => new(Items.Count > Paging.PageSize, Paging.PageNumber > 0, Paging.PageNumber, Paging.PageSize) { };

    [JsonIgnore]
    private Paging Paging { get; } = Paging;

    public static IPagedResult<TItem> Create
        (Paging paging, IQueryable<TItem> source)
        => new PagedResult<TItem>(ApplyPagination(paging, source)?.ToList(), paging);

    private static IQueryable<TItem> ApplyPagination(Paging paging, IQueryable<TItem> source)
        => source.Skip(paging.PageSize * (paging.PageNumber - 1)).Take(paging.PageSize + 1);
}