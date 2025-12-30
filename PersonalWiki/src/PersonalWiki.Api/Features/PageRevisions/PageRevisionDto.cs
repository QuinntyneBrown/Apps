using PersonalWiki.Core;

namespace PersonalWiki.Api.Features.PageRevisions;

public record PageRevisionDto
{
    public Guid PageRevisionId { get; init; }
    public Guid WikiPageId { get; init; }
    public int Version { get; init; }
    public string Content { get; init; } = string.Empty;
    public string? ChangeSummary { get; init; }
    public string? RevisedBy { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class PageRevisionExtensions
{
    public static PageRevisionDto ToDto(this PageRevision revision)
    {
        return new PageRevisionDto
        {
            PageRevisionId = revision.PageRevisionId,
            WikiPageId = revision.WikiPageId,
            Version = revision.Version,
            Content = revision.Content,
            ChangeSummary = revision.ChangeSummary,
            RevisedBy = revision.RevisedBy,
            CreatedAt = revision.CreatedAt,
        };
    }
}
