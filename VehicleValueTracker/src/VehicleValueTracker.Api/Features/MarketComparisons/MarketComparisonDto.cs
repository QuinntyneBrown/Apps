using VehicleValueTracker.Core;

namespace VehicleValueTracker.Api.Features.MarketComparisons;

public record MarketComparisonDto
{
    public Guid MarketComparisonId { get; init; }
    public Guid VehicleId { get; init; }
    public DateTime ComparisonDate { get; init; }
    public string ListingSource { get; init; } = string.Empty;
    public int ComparableYear { get; init; }
    public string ComparableMake { get; init; } = string.Empty;
    public string ComparableModel { get; init; } = string.Empty;
    public string? ComparableTrim { get; init; }
    public decimal ComparableMileage { get; init; }
    public decimal AskingPrice { get; init; }
    public string? Location { get; init; }
    public string? Condition { get; init; }
    public string? ListingUrl { get; init; }
    public int? DaysOnMarket { get; init; }
    public string? Notes { get; init; }
    public bool IsActive { get; init; }
}

public static class MarketComparisonExtensions
{
    public static MarketComparisonDto ToDto(this MarketComparison comparison)
    {
        return new MarketComparisonDto
        {
            MarketComparisonId = comparison.MarketComparisonId,
            VehicleId = comparison.VehicleId,
            ComparisonDate = comparison.ComparisonDate,
            ListingSource = comparison.ListingSource,
            ComparableYear = comparison.ComparableYear,
            ComparableMake = comparison.ComparableMake,
            ComparableModel = comparison.ComparableModel,
            ComparableTrim = comparison.ComparableTrim,
            ComparableMileage = comparison.ComparableMileage,
            AskingPrice = comparison.AskingPrice,
            Location = comparison.Location,
            Condition = comparison.Condition,
            ListingUrl = comparison.ListingUrl,
            DaysOnMarket = comparison.DaysOnMarket,
            Notes = comparison.Notes,
            IsActive = comparison.IsActive,
        };
    }
}
