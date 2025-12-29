namespace MovieTVShowWatchlist.Api;

public record UpdateRatingRequest(
    decimal RatingValue,
    string? Mood
);
