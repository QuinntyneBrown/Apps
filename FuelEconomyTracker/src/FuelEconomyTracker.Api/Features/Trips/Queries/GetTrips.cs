// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.Trips.Queries;

/// <summary>
/// Query to get all trips.
/// </summary>
public class GetTrips : IRequest<List<TripDto>>
{
    public Guid? VehicleId { get; set; }
}

/// <summary>
/// Handler for GetTrips query.
/// </summary>
public class GetTripsHandler : IRequestHandler<GetTrips, List<TripDto>>
{
    private readonly IFuelEconomyTrackerContext _context;

    public GetTripsHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<TripDto>> Handle(GetTrips request, CancellationToken cancellationToken)
    {
        var query = _context.Trips.AsQueryable();

        if (request.VehicleId.HasValue)
        {
            query = query.Where(t => t.VehicleId == request.VehicleId.Value);
        }

        var trips = await query
            .OrderByDescending(t => t.StartDate)
            .ToListAsync(cancellationToken);

        return trips.Select(t => new TripDto
        {
            TripId = t.TripId,
            VehicleId = t.VehicleId,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            StartOdometer = t.StartOdometer,
            EndOdometer = t.EndOdometer,
            Distance = t.Distance,
            Purpose = t.Purpose,
            StartLocation = t.StartLocation,
            EndLocation = t.EndLocation,
            AverageMPG = t.AverageMPG,
            Notes = t.Notes
        }).ToList();
    }
}
