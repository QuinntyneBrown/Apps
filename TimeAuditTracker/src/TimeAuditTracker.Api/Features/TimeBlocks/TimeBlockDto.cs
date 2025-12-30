using TimeAuditTracker.Core;

namespace TimeAuditTracker.Api.Features.TimeBlocks;

public record TimeBlockDto
{
    public Guid TimeBlockId { get; init; }
    public Guid UserId { get; init; }
    public ActivityCategory Category { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string? Notes { get; init; }
    public string? Tags { get; init; }
    public bool IsProductive { get; init; }
    public DateTime CreatedAt { get; init; }
    public double? DurationInMinutes { get; init; }
}

public static class TimeBlockExtensions
{
    public static TimeBlockDto ToDto(this TimeBlock timeBlock)
    {
        return new TimeBlockDto
        {
            TimeBlockId = timeBlock.TimeBlockId,
            UserId = timeBlock.UserId,
            Category = timeBlock.Category,
            Description = timeBlock.Description,
            StartTime = timeBlock.StartTime,
            EndTime = timeBlock.EndTime,
            Notes = timeBlock.Notes,
            Tags = timeBlock.Tags,
            IsProductive = timeBlock.IsProductive,
            CreatedAt = timeBlock.CreatedAt,
            DurationInMinutes = timeBlock.GetDurationInMinutes(),
        };
    }
}
