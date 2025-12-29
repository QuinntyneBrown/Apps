namespace MovieTVShowWatchlist.Api;

public record AbandonContentRequest(
    DateTime AbandonDate,
    decimal ProgressPercent,
    string? AbandonReason,
    decimal? QualityRating,
    bool WouldRetry
);
