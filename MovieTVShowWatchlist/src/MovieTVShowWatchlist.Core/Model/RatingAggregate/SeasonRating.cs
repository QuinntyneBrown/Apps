namespace MovieTVShowWatchlist.Core;

public class SeasonRating
{
    public Guid SeasonRatingId { get; set; }
    public Guid RatingId { get; set; }
    public Guid TVShowId { get; set; }
    public int SeasonNumber { get; set; }
    public decimal RatingValue { get; set; }
    public DateTime CreatedAt { get; set; }

    public Rating Rating { get; set; } = null!;
}
