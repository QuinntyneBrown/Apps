namespace MovieTVShowWatchlist.Core;

public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<WatchlistItem> WatchlistItems { get; set; } = new List<WatchlistItem>();
    public ICollection<ViewingRecord> ViewingRecords { get; set; } = new List<ViewingRecord>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<StreamingSubscription> StreamingSubscriptions { get; set; } = new List<StreamingSubscription>();
}
