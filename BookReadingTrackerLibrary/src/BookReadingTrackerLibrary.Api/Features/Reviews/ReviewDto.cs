using BookReadingTrackerLibrary.Core;

namespace BookReadingTrackerLibrary.Api.Features.Reviews;

public record ReviewDto
{
    public Guid ReviewId { get; init; }
    public Guid UserId { get; init; }
    public Guid BookId { get; init; }
    public int Rating { get; init; }
    public string ReviewText { get; init; } = string.Empty;
    public bool IsRecommended { get; init; }
    public DateTime ReviewDate { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ReviewExtensions
{
    public static ReviewDto ToDto(this Review review)
    {
        return new ReviewDto
        {
            ReviewId = review.ReviewId,
            UserId = review.UserId,
            BookId = review.BookId,
            Rating = review.Rating,
            ReviewText = review.ReviewText,
            IsRecommended = review.IsRecommended,
            ReviewDate = review.ReviewDate,
            CreatedAt = review.CreatedAt,
        };
    }
}
