namespace MovieTVShowWatchlist.Core;

public class WatchPartyParticipant
{
    public Guid WatchPartyParticipantId { get; set; }
    public Guid WatchPartyId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public ParticipantStatus Status { get; set; }
    public DateTime InvitedAt { get; set; }
    public DateTime? RespondedAt { get; set; }

    public WatchParty WatchParty { get; set; } = null!;
    public User User { get; set; } = null!;
}
