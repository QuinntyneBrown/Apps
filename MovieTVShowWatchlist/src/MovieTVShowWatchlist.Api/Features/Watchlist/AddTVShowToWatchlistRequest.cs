namespace MovieTVShowWatchlist.Api;

public record AddTVShowToWatchlistRequest(
    Guid ShowId,
    string Title,
    int PremiereYear,
    List<string>? Genres,
    int? NumberOfSeasons,
    string? Status,
    string? Priority,
    string? StreamingPlatform
);
