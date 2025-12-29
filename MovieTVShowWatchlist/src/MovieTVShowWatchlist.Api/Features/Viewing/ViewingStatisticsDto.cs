namespace MovieTVShowWatchlist.Api;

public record ViewingStatisticsDto(
    int TotalMoviesWatched,
    int TotalShowsWatched,
    int TotalEpisodesWatched,
    int TotalHoursWatched,
    string? GenreBreakdown,
    string? PlatformBreakdown
);
