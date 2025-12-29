namespace MovieTVShowWatchlist.Core;

public class Rating
{
    public Guid RatingId { get; set; }
    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
    public decimal RatingValue { get; set; }
    public RatingScale RatingScale { get; set; }
    public DateTime RatingDate { get; set; }
    public DateTime? ViewingDate { get; set; }
    public bool IsRewatchRating { get; set; }
    public string? Mood { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public ICollection<SeasonRating> SeasonRatings { get; set; } = new List<SeasonRating>();
}
