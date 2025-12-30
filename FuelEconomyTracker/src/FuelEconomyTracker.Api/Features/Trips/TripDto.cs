// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Api.Features.Trips;

/// <summary>
/// Data transfer object for Trip.
/// </summary>
public class TripDto
{
    public Guid TripId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal StartOdometer { get; set; }
    public decimal? EndOdometer { get; set; }
    public decimal? Distance { get; set; }
    public string? Purpose { get; set; }
    public string? StartLocation { get; set; }
    public string? EndLocation { get; set; }
    public decimal? AverageMPG { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Request to create a new trip.
/// </summary>
public class CreateTripRequest
{
    public Guid VehicleId { get; set; }
    public DateTime StartDate { get; set; }
    public decimal StartOdometer { get; set; }
    public string? Purpose { get; set; }
    public string? StartLocation { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Request to update an existing trip.
/// </summary>
public class UpdateTripRequest
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal StartOdometer { get; set; }
    public decimal? EndOdometer { get; set; }
    public string? Purpose { get; set; }
    public string? StartLocation { get; set; }
    public string? EndLocation { get; set; }
    public decimal? AverageMPG { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Request to complete a trip.
/// </summary>
public class CompleteTripRequest
{
    public DateTime EndDate { get; set; }
    public decimal EndOdometer { get; set; }
    public string? EndLocation { get; set; }
}
