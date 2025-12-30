using PerformanceReviewPrepTool.Core;

namespace PerformanceReviewPrepTool.Api.Features.ReviewPeriods;

public record ReviewPeriodDto
{
    public Guid ReviewPeriodId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime? ReviewDueDate { get; init; }
    public string? ReviewerName { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletedDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class ReviewPeriodExtensions
{
    public static ReviewPeriodDto ToDto(this ReviewPeriod reviewPeriod)
    {
        return new ReviewPeriodDto
        {
            ReviewPeriodId = reviewPeriod.ReviewPeriodId,
            UserId = reviewPeriod.UserId,
            Title = reviewPeriod.Title,
            StartDate = reviewPeriod.StartDate,
            EndDate = reviewPeriod.EndDate,
            ReviewDueDate = reviewPeriod.ReviewDueDate,
            ReviewerName = reviewPeriod.ReviewerName,
            IsCompleted = reviewPeriod.IsCompleted,
            CompletedDate = reviewPeriod.CompletedDate,
            Notes = reviewPeriod.Notes,
            CreatedAt = reviewPeriod.CreatedAt,
            UpdatedAt = reviewPeriod.UpdatedAt,
        };
    }
}
