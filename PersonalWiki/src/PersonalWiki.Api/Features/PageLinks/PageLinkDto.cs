using PersonalWiki.Core;

namespace PersonalWiki.Api.Features.PageLinks;

public record PageLinkDto
{
    public Guid PageLinkId { get; init; }
    public Guid SourcePageId { get; init; }
    public Guid TargetPageId { get; init; }
    public string? AnchorText { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class PageLinkExtensions
{
    public static PageLinkDto ToDto(this PageLink link)
    {
        return new PageLinkDto
        {
            PageLinkId = link.PageLinkId,
            SourcePageId = link.SourcePageId,
            TargetPageId = link.TargetPageId,
            AnchorText = link.AnchorText,
            CreatedAt = link.CreatedAt,
        };
    }
}
