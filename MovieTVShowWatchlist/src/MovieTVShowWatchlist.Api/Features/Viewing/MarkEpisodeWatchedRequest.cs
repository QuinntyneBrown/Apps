namespace MovieTVShowWatchlist.Api;

public record MarkEpisodeWatchedRequest(
    DateTime WatchDate,
    string? Platform,
    Guid? BingeSessionId,
    int? ViewingDurationMinutes
);
