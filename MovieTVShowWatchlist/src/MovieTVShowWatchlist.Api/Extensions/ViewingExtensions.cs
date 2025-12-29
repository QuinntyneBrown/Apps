using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public static class ViewingExtensions
{
    public static ViewingRecordDto ToDto(this ViewingRecord record)
    {
        return new ViewingRecordDto(
            record.ViewingRecordId,
            record.ContentId,
            record.ContentType.ToString(),
            record.WatchDate,
            record.ViewingPlatform,
            record.ViewingLocation,
            record.ViewingContext?.ToString(),
            record.IsRewatch,
            record.ViewingDurationMinutes,
            record.Companions?.Select(c => c.CompanionName).ToList()
        );
    }

    public static ShowProgressDto ToDto(this ShowProgress progress)
    {
        return new ShowProgressDto(
            progress.TVShowId,
            progress.LastWatchedSeason,
            progress.LastWatchedEpisode,
            progress.TotalEpisodesWatched,
            progress.CompletedSeasons,
            progress.IsCompleted,
            progress.CompletionDate
        );
    }

    public static ViewingStatisticsDto ToDto(this ViewingStatistics stats)
    {
        return new ViewingStatisticsDto(
            stats.TotalMoviesWatched,
            stats.TotalShowsWatched,
            stats.TotalEpisodesWatched,
            stats.TotalHoursWatched,
            stats.GenreBreakdown,
            stats.PlatformBreakdown
        );
    }
}
