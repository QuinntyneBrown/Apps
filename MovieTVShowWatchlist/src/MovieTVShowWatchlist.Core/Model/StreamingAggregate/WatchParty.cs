namespace MovieTVShowWatchlist.Core;

public class WatchParty
{
    public Guid WatchPartyId { get; set; }
    public Guid HostUserId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string? Platform { get; set; }
    public string? ViewingContext { get; set; }
    public string? DiscussionPlan { get; set; }
    public WatchPartyStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User Host { get; set; } = null!;
    public ICollection<WatchPartyParticipant> Participants { get; set; } = new List<WatchPartyParticipant>();
}
