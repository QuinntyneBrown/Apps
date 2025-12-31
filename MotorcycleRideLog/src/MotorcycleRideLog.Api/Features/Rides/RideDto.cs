// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;

namespace MotorcycleRideLog.Api.Features.Rides;

public class RideDto
{
    public Guid RideId { get; set; }
    public Guid UserId { get; set; }
    public Guid MotorcycleId { get; set; }
    public Guid? RouteId { get; set; }
    public DateTime RideDate { get; set; }
    public decimal DistanceMiles { get; set; }
    public int? DurationMinutes { get; set; }
    public RideType Type { get; set; }
    public string? StartLocation { get; set; }
    public string? EndLocation { get; set; }
    public WeatherCondition? Weather { get; set; }
    public int? AverageSpeed { get; set; }
    public decimal? FuelUsed { get; set; }
    public string? Notes { get; set; }
    public int? Rating { get; set; }
    public DateTime CreatedAt { get; set; }

    public static RideDto FromEntity(Ride ride)
    {
        return new RideDto
        {
            RideId = ride.RideId,
            UserId = ride.UserId,
            MotorcycleId = ride.MotorcycleId,
            RouteId = ride.RouteId,
            RideDate = ride.RideDate,
            DistanceMiles = ride.DistanceMiles,
            DurationMinutes = ride.DurationMinutes,
            Type = ride.Type,
            StartLocation = ride.StartLocation,
            EndLocation = ride.EndLocation,
            Weather = ride.Weather,
            AverageSpeed = ride.AverageSpeed,
            FuelUsed = ride.FuelUsed,
            Notes = ride.Notes,
            Rating = ride.Rating,
            CreatedAt = ride.CreatedAt
        };
    }
}
