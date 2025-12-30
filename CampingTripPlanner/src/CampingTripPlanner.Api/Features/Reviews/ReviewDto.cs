using CampingTripPlanner.Core;

namespace CampingTripPlanner.Api.Features.Reviews;

public record ReviewDto
{
    public Guid ReviewId { get; init; }
    public Guid UserId { get; init; }
    public Guid CampsiteId { get; init; }
    public int Rating { get; init; }
    public string? ReviewText { get; init; }
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
            CampsiteId = review.CampsiteId,
            Rating = review.Rating,
            ReviewText = review.ReviewText,
            ReviewDate = review.ReviewDate,
            CreatedAt = review.CreatedAt,
        };
    }
}
