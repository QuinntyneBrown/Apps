namespace MarketComparisons.Core.Models;

public class MarketComparison
{
    public Guid ComparisonId { get; set; }
    public Guid VehicleId { get; set; }
    public Guid UserId { get; set; }
    public string Source { get; set; } = string.Empty;
    public decimal MarketPrice { get; set; }
    public string? ListingUrl { get; set; }
    public DateTime ComparisonDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
