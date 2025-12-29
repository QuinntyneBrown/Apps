namespace MovieTVShowWatchlist.Api;

public record MarkMovieWatchedRequest(
    DateTime WatchDate,
    string? ViewingLocation,
    string? ViewingPlatform,
    List<string>? WatchedWith,
    string? ViewingContext,
    bool IsRewatch
);
