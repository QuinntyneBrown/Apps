using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public static class RatingExtensions
{
    public static RatingDto ToDto(this Rating rating)
    {
        return new RatingDto(
            rating.RatingId,
            rating.ContentId,
            rating.ContentType.ToString(),
            rating.RatingValue,
            rating.RatingScale.ToString(),
            rating.RatingDate,
            rating.ViewingDate,
            rating.IsRewatchRating,
            rating.Mood
        );
    }
}
