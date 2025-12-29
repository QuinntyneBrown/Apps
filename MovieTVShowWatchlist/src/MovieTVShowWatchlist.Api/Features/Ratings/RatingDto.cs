namespace MovieTVShowWatchlist.Api;

public record RatingDto(
    Guid RatingId,
    Guid ContentId,
    string ContentType,
    decimal RatingValue,
    string RatingScale,
    DateTime RatingDate,
    DateTime? ViewingDate,
    bool IsRewatchRating,
    string? Mood
);
