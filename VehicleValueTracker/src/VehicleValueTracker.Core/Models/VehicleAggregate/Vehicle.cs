// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core;

/// <summary>
/// Represents a vehicle in the value tracking system.
/// </summary>
public class Vehicle
{
    /// <summary>
    /// Gets or sets the unique identifier for the vehicle.
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the vehicle make (manufacturer).
    /// </summary>
    public string Make { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the vehicle model.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the vehicle year.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the trim level.
    /// </summary>
    public string? Trim { get; set; }

    /// <summary>
    /// Gets or sets the Vehicle Identification Number (VIN).
    /// </summary>
    public string? VIN { get; set; }

    /// <summary>
    /// Gets or sets the current odometer reading in miles.
    /// </summary>
    public decimal CurrentMileage { get; set; }

    /// <summary>
    /// Gets or sets the original purchase price.
    /// </summary>
    public decimal? PurchasePrice { get; set; }

    /// <summary>
    /// Gets or sets the purchase date.
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// Gets or sets the exterior color.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the interior type.
    /// </summary>
    public string? InteriorType { get; set; }

    /// <summary>
    /// Gets or sets the engine type.
    /// </summary>
    public string? EngineType { get; set; }

    /// <summary>
    /// Gets or sets the transmission type.
    /// </summary>
    public string? Transmission { get; set; }

    /// <summary>
    /// Gets or sets whether the vehicle is currently owned.
    /// </summary>
    public bool IsCurrentlyOwned { get; set; } = true;

    /// <summary>
    /// Gets or sets additional notes about the vehicle.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the collection of value assessments for this vehicle.
    /// </summary>
    public List<ValueAssessment> ValueAssessments { get; set; } = new List<ValueAssessment>();

    /// <summary>
    /// Gets or sets the collection of market comparisons for this vehicle.
    /// </summary>
    public List<MarketComparison> MarketComparisons { get; set; } = new List<MarketComparison>();

    /// <summary>
    /// Updates the odometer reading.
    /// </summary>
    /// <param name="mileage">The new mileage reading.</param>
    /// <exception cref="ArgumentException">Thrown when the new mileage is less than the current mileage.</exception>
    public void UpdateMileage(decimal mileage)
    {
        if (mileage < CurrentMileage)
        {
            throw new ArgumentException("New mileage cannot be less than current mileage.", nameof(mileage));
        }

        CurrentMileage = mileage;
    }

    /// <summary>
    /// Marks the vehicle as sold.
    /// </summary>
    public void MarkAsSold()
    {
        IsCurrentlyOwned = false;
    }

    /// <summary>
    /// Gets the most recent value assessment.
    /// </summary>
    /// <returns>The most recent assessment or null if none exist.</returns>
    public ValueAssessment? GetMostRecentAssessment()
    {
        return ValueAssessments
            .OrderByDescending(a => a.AssessmentDate)
            .FirstOrDefault();
    }
}
