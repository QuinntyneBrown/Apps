namespace MovieTVShowWatchlist.Core;

public class BingeSession
{
    public Guid BingeSessionId { get; set; }
    public Guid UserId { get; set; }
    public Guid TVShowId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int EpisodesWatched { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
    public TVShow TVShow { get; set; } = null!;
}
