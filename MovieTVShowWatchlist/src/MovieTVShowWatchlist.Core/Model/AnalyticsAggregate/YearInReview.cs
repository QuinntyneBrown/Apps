namespace MovieTVShowWatchlist.Core;

public class YearInReview
{
    public Guid YearInReviewId { get; set; }
    public Guid UserId { get; set; }
    public int Year { get; set; }
    public int TotalMoviesWatched { get; set; }
    public int TotalShowsWatched { get; set; }
    public int TotalEpisodesWatched { get; set; }
    public int TotalHoursWatched { get; set; }
    public string? FavoriteGenres { get; set; }
    public string? TopRatedContent { get; set; }
    public string? ViewingTrends { get; set; }
    public string? MemorableMoments { get; set; }
    public DateTime GeneratedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
}
