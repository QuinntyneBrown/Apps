// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RoadsideAssistanceInfoHub.Core;

/// <summary>
/// Represents a vehicle in the roadside assistance system.
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
    /// Gets or sets the vehicle color.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the current odometer reading.
    /// </summary>
    public decimal? CurrentMileage { get; set; }

    /// <summary>
    /// Gets or sets the owner name.
    /// </summary>
    public string? OwnerName { get; set; }

    /// <summary>
    /// Gets or sets additional notes about the vehicle.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets whether the vehicle is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the collection of insurance information for this vehicle.
    /// </summary>
    public List<InsuranceInfo> InsuranceInfos { get; set; } = new List<InsuranceInfo>();

    /// <summary>
    /// Gets or sets the collection of policies for this vehicle.
    /// </summary>
    public List<Policy> Policies { get; set; } = new List<Policy>();

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

    /// <summary>
    /// Updates the owner information.
    /// </summary>
    /// <param name="ownerName">The new owner name.</param>
    public void UpdateOwner(string ownerName)
    {
        OwnerName = ownerName;
    }
}
