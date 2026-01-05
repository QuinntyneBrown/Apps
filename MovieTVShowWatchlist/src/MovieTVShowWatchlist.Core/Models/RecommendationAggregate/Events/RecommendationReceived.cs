namespace MovieTVShowWatchlist.Core;

public class RecommendationReceived : DomainEvent
{
    public Guid RecommendationId { get; set; }
    public Guid ContentId { get; set; }
    public Guid? RecommenderId { get; set; }
    public string RecommendationSource { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public DateTime ReceptionDate { get; set; }
    public string? InterestLevel { get; set; }
    public bool AddedToWatchlist { get; set; }
}
