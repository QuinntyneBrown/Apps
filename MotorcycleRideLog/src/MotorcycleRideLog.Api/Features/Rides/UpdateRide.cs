// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Rides;

public class UpdateRide
{
    public class Command : IRequest<RideDto>
    {
        public Guid RideId { get; set; }
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
    }

    public class Handler : IRequestHandler<Command, RideDto>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<RideDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var ride = await _context.Rides
                .FirstOrDefaultAsync(x => x.RideId == request.RideId, cancellationToken)
                ?? throw new KeyNotFoundException($"Ride with ID {request.RideId} not found.");

            ride.MotorcycleId = request.MotorcycleId;
            ride.RouteId = request.RouteId;
            ride.RideDate = request.RideDate;
            ride.DistanceMiles = request.DistanceMiles;
            ride.DurationMinutes = request.DurationMinutes;
            ride.Type = request.Type;
            ride.StartLocation = request.StartLocation;
            ride.EndLocation = request.EndLocation;
            ride.Weather = request.Weather;
            ride.AverageSpeed = request.AverageSpeed;
            ride.FuelUsed = request.FuelUsed;
            ride.Notes = request.Notes;
            ride.Rating = request.Rating;

            await _context.SaveChangesAsync(cancellationToken);

            return RideDto.FromEntity(ride);
        }
    }
}
