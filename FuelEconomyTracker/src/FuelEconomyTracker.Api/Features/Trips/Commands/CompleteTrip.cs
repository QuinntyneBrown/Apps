// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.Trips.Commands;

/// <summary>
/// Command to complete a trip.
/// </summary>
public class CompleteTrip : IRequest<TripDto>
{
    public Guid TripId { get; set; }
    public DateTime EndDate { get; set; }
    public decimal EndOdometer { get; set; }
    public string? EndLocation { get; set; }
}

/// <summary>
/// Handler for CompleteTrip command.
/// </summary>
public class CompleteTripHandler : IRequestHandler<CompleteTrip, TripDto>
{
    private readonly IFuelEconomyTrackerContext _context;

    public CompleteTripHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<TripDto> Handle(CompleteTrip request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            throw new KeyNotFoundException($"Trip with ID {request.TripId} not found.");
        }

        trip.CompleteTrip(request.EndDate, request.EndOdometer);
        trip.EndLocation = request.EndLocation;

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
