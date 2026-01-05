namespace MovieTVShowWatchlist.Core;

public class ViewingCompanion
{
    public Guid ViewingCompanionId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid ViewingRecordId { get; set; }
    public string CompanionName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ViewingRecord ViewingRecord { get; set; } = null!;
}
