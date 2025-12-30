// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Api.Features.Vehicles;

/// <summary>
/// Data transfer object for Vehicle.
/// </summary>
public class VehicleDto
{
    public Guid VehicleId { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? VIN { get; set; }
    public string? LicensePlate { get; set; }
    public decimal? TankCapacity { get; set; }
    public decimal? EPACityMPG { get; set; }
    public decimal? EPAHighwayMPG { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// Request to create a new vehicle.
/// </summary>
public class CreateVehicleRequest
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? VIN { get; set; }
    public string? LicensePlate { get; set; }
    public decimal? TankCapacity { get; set; }
    public decimal? EPACityMPG { get; set; }
    public decimal? EPAHighwayMPG { get; set; }
}

/// <summary>
/// Request to update an existing vehicle.
/// </summary>
public class UpdateVehicleRequest
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? VIN { get; set; }
    public string? LicensePlate { get; set; }
    public decimal? TankCapacity { get; set; }
    public decimal? EPACityMPG { get; set; }
    public decimal? EPAHighwayMPG { get; set; }
    public bool IsActive { get; set; }
}
