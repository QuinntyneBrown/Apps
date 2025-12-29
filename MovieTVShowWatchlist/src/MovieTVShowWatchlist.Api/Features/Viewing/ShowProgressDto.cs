namespace MovieTVShowWatchlist.Api;

public record ShowProgressDto(
    Guid TVShowId,
    int? LastWatchedSeason,
    int? LastWatchedEpisode,
    int TotalEpisodesWatched,
    int CompletedSeasons,
    bool IsCompleted,
    DateTime? CompletionDate
);
