namespace Watchlists.Core;

public class WatchlistItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ReleaseYear { get; set; }
    public string Status { get; set; } = "Unwatched";
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public DateTime? WatchedAt { get; set; }
}
