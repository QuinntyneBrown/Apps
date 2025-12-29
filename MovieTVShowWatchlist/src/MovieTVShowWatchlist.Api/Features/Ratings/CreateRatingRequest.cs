namespace MovieTVShowWatchlist.Api;

public record CreateRatingRequest(
    decimal RatingValue,
    string RatingScale,
    DateTime RatingDate,
    DateTime? ViewingDate,
    bool IsRewatchRating,
    string? Mood
);
