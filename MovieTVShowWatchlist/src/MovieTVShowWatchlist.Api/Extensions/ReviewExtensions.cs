using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public static class ReviewExtensions
{
    public static ReviewDto ToDto(this Review review)
    {
        return new ReviewDto(
            review.ReviewId,
            review.ContentId,
            review.ContentType.ToString(),
            review.ReviewText,
            review.HasSpoilers,
            review.ReviewDate,
            review.WouldRecommend,
            review.TargetAudience,
            review.Themes?.Select(t => t.Theme).ToList()
        );
    }
}
