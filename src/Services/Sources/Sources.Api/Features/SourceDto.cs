using Sources.Core.Models;

namespace Sources.Api.Features;

public record SourceDto(
    Guid SourceId,
    Guid UserId,
    string Title,
    string SourceType,
    string? Author,
    string? Url,
    string? Notes,
    DateTime CreatedAt);

public static class SourceExtensions
{
    public static SourceDto ToDto(this Source source)
    {
        return new SourceDto(
            source.SourceId,
            source.UserId,
            source.Title,
            source.SourceType,
            source.Author,
            source.Url,
            source.Notes,
            source.CreatedAt);
    }
}
