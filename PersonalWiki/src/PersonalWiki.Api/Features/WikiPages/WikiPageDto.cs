using PersonalWiki.Core;

namespace PersonalWiki.Api.Features.WikiPages;

public record WikiPageDto
{
    public Guid WikiPageId { get; init; }
    public Guid UserId { get; init; }
    public Guid? CategoryId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public PageStatus Status { get; init; }
    public int Version { get; init; }
    public bool IsFeatured { get; init; }
    public int ViewCount { get; init; }
    public DateTime LastModifiedAt { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class WikiPageExtensions
{
    public static WikiPageDto ToDto(this WikiPage page)
    {
        return new WikiPageDto
        {
            WikiPageId = page.WikiPageId,
            UserId = page.UserId,
            CategoryId = page.CategoryId,
            Title = page.Title,
            Slug = page.Slug,
            Content = page.Content,
            Status = page.Status,
            Version = page.Version,
            IsFeatured = page.IsFeatured,
            ViewCount = page.ViewCount,
            LastModifiedAt = page.LastModifiedAt,
            CreatedAt = page.CreatedAt,
        };
    }
}
