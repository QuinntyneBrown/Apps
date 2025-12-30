using PerformanceReviewPrepTool.Core;

namespace PerformanceReviewPrepTool.Api.Features.Feedbacks;

public record FeedbackDto
{
    public Guid FeedbackId { get; init; }
    public Guid UserId { get; init; }
    public Guid ReviewPeriodId { get; init; }
    public string Source { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime ReceivedDate { get; init; }
    public string? FeedbackType { get; init; }
    public string? Category { get; init; }
    public bool IsKeyFeedback { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class FeedbackExtensions
{
    public static FeedbackDto ToDto(this Feedback feedback)
    {
        return new FeedbackDto
        {
            FeedbackId = feedback.FeedbackId,
            UserId = feedback.UserId,
            ReviewPeriodId = feedback.ReviewPeriodId,
            Source = feedback.Source,
            Content = feedback.Content,
            ReceivedDate = feedback.ReceivedDate,
            FeedbackType = feedback.FeedbackType,
            Category = feedback.Category,
            IsKeyFeedback = feedback.IsKeyFeedback,
            Notes = feedback.Notes,
            CreatedAt = feedback.CreatedAt,
            UpdatedAt = feedback.UpdatedAt,
        };
    }
}
