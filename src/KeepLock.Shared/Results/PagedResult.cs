using KeepLock.Domain.Models.Common.Primitives.Interfaces;
using KeepLock.Domain.Models.Common.Primitives.records;
using System.Text.Json.Serialization;

namespace KeepLock.Application.Common.Models;

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