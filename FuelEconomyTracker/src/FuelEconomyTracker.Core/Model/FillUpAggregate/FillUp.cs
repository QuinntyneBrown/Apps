// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Core;

/// <summary>
/// Represents a fuel fill-up record.
/// </summary>
public class FillUp
{
    /// <summary>
    /// Gets or sets the unique identifier for the fill-up.
    /// </summary>
    public Guid FillUpId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the vehicle.
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Gets or sets the date of the fill-up.
    /// </summary>
    public DateTime FillUpDate { get; set; }

    /// <summary>
    /// Gets or sets the odometer reading at fill-up.
    /// </summary>
    public decimal Odometer { get; set; }

    /// <summary>
    /// Gets or sets the number of gallons filled.
    /// </summary>
    public decimal Gallons { get; set; }

    /// <summary>
    /// Gets or sets the price per gallon.
    /// </summary>
    public decimal PricePerGallon { get; set; }

    /// <summary>
    /// Gets or sets the total cost of the fill-up.
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Gets or sets whether this was a full tank fill-up.
    /// </summary>
    public bool IsFullTank { get; set; }

    /// <summary>
    /// Gets or sets the fuel grade (Regular, Premium, etc.).
    /// </summary>
    public string? FuelGrade { get; set; }

    /// <summary>
    /// Gets or sets the gas station name.
    /// </summary>
    public string? GasStation { get; set; }

    /// <summary>
    /// Gets or sets the calculated miles per gallon.
    /// </summary>
    public decimal? MilesPerGallon { get; set; }

    /// <summary>
    /// Gets or sets additional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the vehicle.
    /// </summary>
    public Vehicle? Vehicle { get; set; }

    /// <summary>
    /// Calculates the total cost based on gallons and price per gallon.
    /// </summary>
    public void CalculateTotalCost()
    {
        TotalCost = Gallons * PricePerGallon;
    }

    /// <summary>
    /// Calculates miles per gallon based on distance traveled and gallons used.
    /// </summary>
    /// <param name="previousOdometer">The odometer reading from the previous fill-up.</param>
    public void CalculateMPG(decimal previousOdometer)
    {
        if (Gallons > 0 && Odometer > previousOdometer)
        {
            decimal distance = Odometer - previousOdometer;
            MilesPerGallon = Math.Round(distance / Gallons, 2);
        }
    }
}
