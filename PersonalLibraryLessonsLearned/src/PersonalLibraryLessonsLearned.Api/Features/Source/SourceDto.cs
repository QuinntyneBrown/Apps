using PersonalLibraryLessonsLearned.Core;

namespace PersonalLibraryLessonsLearned.Api.Features.Source;

public record SourceDto
{
    public Guid SourceId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Author { get; init; }
    public string SourceType { get; init; } = string.Empty;
    public string? Url { get; init; }
    public DateTime? DateConsumed { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class SourceExtensions
{
    public static SourceDto ToDto(this Core.Source source)
    {
        return new SourceDto
        {
            SourceId = source.SourceId,
            UserId = source.UserId,
            Title = source.Title,
            Author = source.Author,
            SourceType = source.SourceType,
            Url = source.Url,
            DateConsumed = source.DateConsumed,
            Notes = source.Notes,
            CreatedAt = source.CreatedAt,
        };
    }
}
