// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Represents a vehicle in the maintenance tracking system.
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
    /// Gets or sets the Vehicle Identification Number (VIN).
    /// </summary>
    public string? VIN { get; set; }

    /// <summary>
    /// Gets or sets the license plate number.
    /// </summary>
    public string? LicensePlate { get; set; }

    /// <summary>
    /// Gets or sets the type of vehicle.
    /// </summary>
    public VehicleType VehicleType { get; set; }

    /// <summary>
    /// Gets or sets the current odometer reading in miles.
    /// </summary>
    public decimal CurrentMileage { get; set; }

    /// <summary>
    /// Gets or sets the purchase date.
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// Gets or sets additional notes about the vehicle.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets whether the vehicle is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the collection of service records for this vehicle.
    /// </summary>
    public List<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();

    /// <summary>
    /// Gets or sets the collection of maintenance schedules for this vehicle.
    /// </summary>
    public List<MaintenanceSchedule> MaintenanceSchedules { get; set; } = new List<MaintenanceSchedule>();

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
