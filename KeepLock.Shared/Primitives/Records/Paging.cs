namespace KeepLock.Shared.Primitives.Records;

public record Paging
{
    private const int UpperSize = 100;
    private const int DefaultPageSize = 10;
    private const int DefaultPageNumber = 1;
    private const int Zero = 0;

    public Paging(int size = DefaultPageSize, int number = DefaultPageNumber)
    {
        PageSize = size switch
        {
            Zero => DefaultPageSize,
            > UpperSize => UpperSize,
            _ => size
        };

        PageNumber = number is Zero
            ? DefaultPageNumber
            : number;
    }

    public int PageSize { get; }
    public int PageNumber { get; }
}