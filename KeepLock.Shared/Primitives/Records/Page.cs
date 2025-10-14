namespace KeepLock.Shared.Primitives.Records;

public record Page(bool HasNext, bool HasPrevious, int Number = 1, int Size = 10) {}