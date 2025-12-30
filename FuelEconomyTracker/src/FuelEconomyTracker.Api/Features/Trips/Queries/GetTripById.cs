// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.Trips.Queries;

/// <summary>
/// Query to get a trip by ID.
/// </summary>
public class GetTripById : IRequest<TripDto?>
{
    public Guid TripId { get; set; }
}

/// <summary>
/// Handler for GetTripById query.
/// </summary>
public class GetTripByIdHandler : IRequestHandler<GetTripById, TripDto?>
{
    private readonly IFuelEconomyTrackerContext _context;

    public GetTripByIdHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<TripDto?> Handle(GetTripById request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            return null;
        }

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
