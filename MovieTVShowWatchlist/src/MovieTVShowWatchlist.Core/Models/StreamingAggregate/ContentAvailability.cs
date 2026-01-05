namespace MovieTVShowWatchlist.Core;

public class ContentAvailability
{
    public Guid ContentAvailabilityId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
    public string Platform { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public bool SubscriptionRequired { get; set; }
    public string? AvailabilityWindow { get; set; }
    public string? RegionalRestrictions { get; set; }
    public DateTime LastChecked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
