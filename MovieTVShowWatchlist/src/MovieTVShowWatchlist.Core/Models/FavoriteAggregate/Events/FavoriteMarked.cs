namespace MovieTVShowWatchlist.Core;

public class FavoriteMarked : DomainEvent
{
    public Guid FavoriteId { get; set; }
    public Guid ContentId { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public DateTime AddedToFavoritesDate { get; set; }
    public string? FavoriteCategory { get; set; }
    public int RewatchCount { get; set; }
    public string? EmotionalSignificance { get; set; }
}
