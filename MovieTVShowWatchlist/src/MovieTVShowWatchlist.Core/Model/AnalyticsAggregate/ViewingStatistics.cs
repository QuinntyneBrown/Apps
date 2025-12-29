namespace MovieTVShowWatchlist.Core;

public class ViewingStatistics
{
    public Guid ViewingStatisticsId { get; set; }
    public Guid UserId { get; set; }
    public StatisticsPeriod Period { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public int TotalMoviesWatched { get; set; }
    public int TotalShowsWatched { get; set; }
    public int TotalEpisodesWatched { get; set; }
    public int TotalHoursWatched { get; set; }
    public string? GenreBreakdown { get; set; }
    public string? PlatformBreakdown { get; set; }
    public DateTime CalculatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; } = null!;
}
