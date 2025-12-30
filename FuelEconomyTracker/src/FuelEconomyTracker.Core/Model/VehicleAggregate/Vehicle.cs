// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Core;

/// <summary>
/// Represents a vehicle in the fuel economy tracking system.
/// </summary>
public class Vehicle
{
    /// <summary>
    /// Gets or sets the unique identifier for the vehicle.
    /// </summary>
    public Guid VehicleId { get; set; }

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
    /// Gets or sets the Vehicle Identification Number (VIN).
    /// </summary>
    public string? VIN { get; set; }

    /// <summary>
    /// Gets or sets the license plate number.
    /// </summary>
    public string? LicensePlate { get; set; }

    /// <summary>
    /// Gets or sets the fuel tank capacity in gallons.
    /// </summary>
    public decimal? TankCapacity { get; set; }

    /// <summary>
    /// Gets or sets the EPA estimated city MPG.
    /// </summary>
    public decimal? EPACityMPG { get; set; }

    /// <summary>
    /// Gets or sets the EPA estimated highway MPG.
    /// </summary>
    public decimal? EPAHighwayMPG { get; set; }

    /// <summary>
    /// Gets or sets whether the vehicle is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the collection of fill-ups for this vehicle.
    /// </summary>
    public List<FillUp> FillUps { get; set; } = new List<FillUp>();

    /// <summary>
    /// Gets or sets the collection of trips for this vehicle.
    /// </summary>
    public List<Trip> Trips { get; set; } = new List<Trip>();

    /// <summary>
    /// Gets or sets the collection of efficiency reports for this vehicle.
    /// </summary>
    public List<EfficiencyReport> EfficiencyReports { get; set; } = new List<EfficiencyReport>();

    /// <summary>
    /// Calculates the overall average MPG based on all fill-ups.
    /// </summary>
    /// <returns>The average MPG or null if no fill-ups exist.</returns>
    public decimal? CalculateOverallMPG()
    {
        var fillUpsWithMPG = FillUps.Where(f => f.MilesPerGallon.HasValue).ToList();
        if (!fillUpsWithMPG.Any())
        {
            return null;
        }

        return Math.Round(fillUpsWithMPG.Average(f => f.MilesPerGallon!.Value), 2);
    }

    /// <summary>
    /// Deactivates the vehicle.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }

    /// <summary>
    /// Reactivates the vehicle.
    /// </summary>
    public void Reactivate()
    {
        IsActive = true;
    }
}
