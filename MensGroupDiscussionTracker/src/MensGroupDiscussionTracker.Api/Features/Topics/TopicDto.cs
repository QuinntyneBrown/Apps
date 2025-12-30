using MensGroupDiscussionTracker.Core;

namespace MensGroupDiscussionTracker.Api.Features.Topics;

public record TopicDto
{
    public Guid TopicId { get; init; }
    public Guid? MeetingId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public TopicCategory Category { get; init; }
    public string? DiscussionNotes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class TopicExtensions
{
    public static TopicDto ToDto(this Topic topic)
    {
        return new TopicDto
        {
            TopicId = topic.TopicId,
            MeetingId = topic.MeetingId,
            UserId = topic.UserId,
            Title = topic.Title,
            Description = topic.Description,
            Category = topic.Category,
            DiscussionNotes = topic.DiscussionNotes,
            CreatedAt = topic.CreatedAt,
            UpdatedAt = topic.UpdatedAt,
        };
    }
}
