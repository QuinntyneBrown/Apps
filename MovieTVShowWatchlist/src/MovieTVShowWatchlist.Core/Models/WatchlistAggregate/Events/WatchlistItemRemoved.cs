namespace MovieTVShowWatchlist.Core;

public class WatchlistItemRemoved : DomainEvent
{
    public Guid ItemId { get; set; }
    public string ItemType { get; set; } = string.Empty;
    public DateTime RemovalDate { get; set; }
    public string? RemovalReason { get; set; }
    public TimeSpan TimeOnWatchlist { get; set; }
    public string? AlternativeAdded { get; set; }
}
