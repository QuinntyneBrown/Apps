using BucketListManager.Core;

namespace BucketListManager.Api.Features.Milestones;

public record MilestoneDto
{
    public Guid MilestoneId { get; init; }
    public Guid UserId { get; init; }
    public Guid BucketListItemId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletedDate { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class MilestoneExtensions
{
    public static MilestoneDto ToDto(this Milestone milestone)
    {
        return new MilestoneDto
        {
            MilestoneId = milestone.MilestoneId,
            UserId = milestone.UserId,
            BucketListItemId = milestone.BucketListItemId,
            Title = milestone.Title,
            Description = milestone.Description,
            IsCompleted = milestone.IsCompleted,
            CompletedDate = milestone.CompletedDate,
            CreatedAt = milestone.CreatedAt,
        };
    }
}
