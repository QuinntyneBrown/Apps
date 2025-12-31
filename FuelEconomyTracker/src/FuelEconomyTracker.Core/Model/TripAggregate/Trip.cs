// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Core;

/// <summary>
/// Represents a trip or journey taken by a vehicle.
/// </summary>
public class Trip
{
    /// <summary>
    /// Gets or sets the unique identifier for the trip.
    /// </summary>
    public Guid TripId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the vehicle.
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Gets or sets the trip start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the trip end date.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the starting odometer reading.
    /// </summary>
    public decimal StartOdometer { get; set; }

    /// <summary>
    /// Gets or sets the ending odometer reading.
    /// </summary>
    public decimal? EndOdometer { get; set; }

    /// <summary>
    /// Gets or sets the total distance traveled.
    /// </summary>
    public decimal? Distance { get; set; }

    /// <summary>
    /// Gets or sets the trip purpose or destination.
    /// </summary>
    public string? Purpose { get; set; }

    /// <summary>
    /// Gets or sets the starting location.
    /// </summary>
    public string? StartLocation { get; set; }

    /// <summary>
    /// Gets or sets the ending location.
    /// </summary>
    public string? EndLocation { get; set; }

    /// <summary>
    /// Gets or sets the average fuel economy for the trip.
    /// </summary>
    public decimal? AverageMPG { get; set; }

    /// <summary>
    /// Gets or sets additional notes about the trip.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the vehicle.
    /// </summary>
    public Vehicle? Vehicle { get; set; }

    /// <summary>
    /// Completes the trip by recording the end details.
    /// </summary>
    /// <param name="endDate">The end date.</param>
    /// <param name="endOdometer">The ending odometer reading.</param>
    public void CompleteTrip(DateTime endDate, decimal endOdometer)
    {
        EndDate = endDate;
        EndOdometer = endOdometer;
        CalculateDistance();
    }

    /// <summary>
    /// Calculates the total distance traveled.
    /// </summary>
    public void CalculateDistance()
    {
        if (EndOdometer.HasValue && EndOdometer > StartOdometer)
        {
            Distance = EndOdometer.Value - StartOdometer;
        }
    }

    /// <summary>
    /// Sets the average MPG for the trip.
    /// </summary>
    /// <param name="mpg">The average miles per gallon.</param>
    public void SetAverageMPG(decimal mpg)
    {
        AverageMPG = mpg;
    }
}
