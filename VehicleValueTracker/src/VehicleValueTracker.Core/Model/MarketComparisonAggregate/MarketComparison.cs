// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core;

/// <summary>
/// Represents a market comparison for similar vehicles.
/// </summary>
public class MarketComparison
{
    /// <summary>
    /// Gets or sets the unique identifier for the market comparison.
    /// </summary>
    public Guid MarketComparisonId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the vehicle being compared.
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Gets or sets the comparison date.
    /// </summary>
    public DateTime ComparisonDate { get; set; }

    /// <summary>
    /// Gets or sets the listing source (e.g., "Autotrader", "CarGurus", "eBay").
    /// </summary>
    public string ListingSource { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the comparable vehicle's year.
    /// </summary>
    public int ComparableYear { get; set; }

    /// <summary>
    /// Gets or sets the comparable vehicle's make.
    /// </summary>
    public string ComparableMake { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the comparable vehicle's model.
    /// </summary>
    public string ComparableModel { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the comparable vehicle's trim.
    /// </summary>
    public string? ComparableTrim { get; set; }

    /// <summary>
    /// Gets or sets the comparable vehicle's mileage.
    /// </summary>
    public decimal ComparableMileage { get; set; }

    /// <summary>
    /// Gets or sets the asking price.
    /// </summary>
    public decimal AskingPrice { get; set; }

    /// <summary>
    /// Gets or sets the location of the listing.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the condition description.
    /// </summary>
    public string? Condition { get; set; }

    /// <summary>
    /// Gets or sets the listing URL.
    /// </summary>
    public string? ListingUrl { get; set; }

    /// <summary>
    /// Gets or sets the days on market.
    /// </summary>
    public int? DaysOnMarket { get; set; }

    /// <summary>
    /// Gets or sets additional notes about the comparison.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets whether this listing is still active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the navigation property to the vehicle.
    /// </summary>
    public Vehicle? Vehicle { get; set; }

    /// <summary>
    /// Calculates the price difference compared to a reference price.
    /// </summary>
    /// <param name="referencePrice">The price to compare against.</param>
    /// <returns>The price difference (positive if asking price is higher).</returns>
    public decimal CalculatePriceDifference(decimal referencePrice)
    {
        return AskingPrice - referencePrice;
    }

    /// <summary>
    /// Calculates the price per mile.
    /// </summary>
    /// <returns>The price per mile.</returns>
    public decimal CalculatePricePerMile()
    {
        if (ComparableMileage > 0)
        {
            return Math.Round(AskingPrice / ComparableMileage, 2);
        }

        return 0;
    }

    /// <summary>
    /// Marks the listing as sold or inactive.
    /// </summary>
    public void MarkAsInactive()
    {
        IsActive = false;
    }
}
