using DailyJournalingApp.Core;

namespace DailyJournalingApp.Api.Features.Tags;

public record TagDto
{
    public Guid TagId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Color { get; init; }
    public int UsageCount { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class TagExtensions
{
    public static TagDto ToDto(this Tag tag)
    {
        return new TagDto
        {
            TagId = tag.TagId,
            UserId = tag.UserId,
            Name = tag.Name,
            Color = tag.Color,
            UsageCount = tag.UsageCount,
            CreatedAt = tag.CreatedAt,
        };
    }
}
