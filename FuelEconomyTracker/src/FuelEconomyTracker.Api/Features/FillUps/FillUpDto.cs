// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Api.Features.FillUps;

/// <summary>
/// Data transfer object for FillUp.
/// </summary>
public class FillUpDto
{
    public Guid FillUpId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime FillUpDate { get; set; }
    public decimal Odometer { get; set; }
    public decimal Gallons { get; set; }
    public decimal PricePerGallon { get; set; }
    public decimal TotalCost { get; set; }
    public bool IsFullTank { get; set; }
    public string? FuelGrade { get; set; }
    public string? GasStation { get; set; }
    public decimal? MilesPerGallon { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Request to create a new fill-up.
/// </summary>
public class CreateFillUpRequest
{
    public Guid VehicleId { get; set; }
    public DateTime FillUpDate { get; set; }
    public decimal Odometer { get; set; }
    public decimal Gallons { get; set; }
    public decimal PricePerGallon { get; set; }
    public bool IsFullTank { get; set; }
    public string? FuelGrade { get; set; }
    public string? GasStation { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Request to update an existing fill-up.
/// </summary>
public class UpdateFillUpRequest
{
    public DateTime FillUpDate { get; set; }
    public decimal Odometer { get; set; }
    public decimal Gallons { get; set; }
    public decimal PricePerGallon { get; set; }
    public bool IsFullTank { get; set; }
    public string? FuelGrade { get; set; }
    public string? GasStation { get; set; }
    public string? Notes { get; set; }
}
