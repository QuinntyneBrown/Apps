namespace MovieTVShowWatchlist.Core;

public class ViewingRecord
{
    public Guid ViewingRecordId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
    public DateTime WatchDate { get; set; }
    public string? ViewingPlatform { get; set; }
    public string? ViewingLocation { get; set; }
    public ViewingContext? ViewingContext { get; set; }
    public bool IsRewatch { get; set; }
    public int? ViewingDurationMinutes { get; set; }
    public Guid? BingeSessionId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
    public ICollection<ViewingCompanion> Companions { get; set; } = new List<ViewingCompanion>();
}
