namespace MovieTVShowWatchlist.Core;

public class Recommendation
{
    public Guid RecommendationId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
    public Guid? RecommenderId { get; set; }
    public Guid RecipientId { get; set; }
    public RecommendationSource Source { get; set; }
    public string? Reason { get; set; }
    public DateTime ReceptionDate { get; set; }
    public InterestLevel? InterestLevel { get; set; }
    public bool AddedToWatchlist { get; set; }
    public string? RecipientFeedback { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? Recommender { get; set; }
    public User Recipient { get; set; } = null!;
}
