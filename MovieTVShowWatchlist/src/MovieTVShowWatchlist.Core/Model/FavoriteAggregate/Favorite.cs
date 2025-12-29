namespace MovieTVShowWatchlist.Core;

public class Favorite
{
    public Guid FavoriteId { get; set; }
    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
    public DateTime AddedDate { get; set; }
    public string? FavoriteCategory { get; set; }
    public int RewatchCount { get; set; }
    public string? EmotionalSignificance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; } = null!;
}
