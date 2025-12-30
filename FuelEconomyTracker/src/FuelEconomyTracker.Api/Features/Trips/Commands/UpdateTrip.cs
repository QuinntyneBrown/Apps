// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.Trips.Commands;

/// <summary>
/// Command to update an existing trip.
/// </summary>
public class UpdateTrip : IRequest<TripDto>
{
    public Guid TripId { get; set; }
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
/// Handler for UpdateTrip command.
/// </summary>
public class UpdateTripHandler : IRequestHandler<UpdateTrip, TripDto>
{
    private readonly IFuelEconomyTrackerContext _context;

    public UpdateTripHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<TripDto> Handle(UpdateTrip request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            throw new KeyNotFoundException($"Trip with ID {request.TripId} not found.");
        }

        trip.StartDate = request.StartDate;
        trip.EndDate = request.EndDate;
        trip.StartOdometer = request.StartOdometer;
        trip.EndOdometer = request.EndOdometer;
        trip.Purpose = request.Purpose;
        trip.StartLocation = request.StartLocation;
        trip.EndLocation = request.EndLocation;
        trip.AverageMPG = request.AverageMPG;
        trip.Notes = request.Notes;

        trip.CalculateDistance();

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
