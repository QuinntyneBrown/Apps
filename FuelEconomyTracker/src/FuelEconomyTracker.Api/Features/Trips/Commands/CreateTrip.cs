// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;

namespace FuelEconomyTracker.Api.Features.Trips.Commands;

/// <summary>
/// Command to create a new trip.
/// </summary>
public class CreateTrip : IRequest<TripDto>
{
    public Guid VehicleId { get; set; }
    public DateTime StartDate { get; set; }
    public decimal StartOdometer { get; set; }
    public string? Purpose { get; set; }
    public string? StartLocation { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreateTrip command.
/// </summary>
public class CreateTripHandler : IRequestHandler<CreateTrip, TripDto>
{
    private readonly IFuelEconomyTrackerContext _context;

    public CreateTripHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<TripDto> Handle(CreateTrip request, CancellationToken cancellationToken)
    {
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            StartDate = request.StartDate,
            StartOdometer = request.StartOdometer,
            Purpose = request.Purpose,
            StartLocation = request.StartLocation,
            Notes = request.Notes
        };

        _context.Trips.Add(trip);
        await _context.SaveChangesAsync(cancellationToken);

        return new TripDto
        {
            TripId = trip.TripId,
            VehicleId = trip.VehicleId,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            StartOdometer = trip.StartOdometer,
            EndOdometer = trip.EndOdometer,
            Distance = trip.Distance,
            Purpose = trip.Purpose,
            StartLocation = trip.StartLocation,
            EndLocation = trip.EndLocation,
            AverageMPG = trip.AverageMPG,
            Notes = trip.Notes
        };
    }
}
