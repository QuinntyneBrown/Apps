// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;

namespace MotorcycleRideLog.Api.Features.Rides;

public class CreateRide
{
    public class Command : IRequest<RideDto>
    {
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
            var ride = new Ride
            {
                RideId = Guid.NewGuid(),
                UserId = request.UserId,
                MotorcycleId = request.MotorcycleId,
                RouteId = request.RouteId,
                RideDate = request.RideDate,
                DistanceMiles = request.DistanceMiles,
                DurationMinutes = request.DurationMinutes,
                Type = request.Type,
                StartLocation = request.StartLocation,
                EndLocation = request.EndLocation,
                Weather = request.Weather,
                AverageSpeed = request.AverageSpeed,
                FuelUsed = request.FuelUsed,
                Notes = request.Notes,
                Rating = request.Rating,
                CreatedAt = DateTime.UtcNow
            };

            _context.Rides.Add(ride);
            await _context.SaveChangesAsync(cancellationToken);

            return RideDto.FromEntity(ride);
        }
    }
}
