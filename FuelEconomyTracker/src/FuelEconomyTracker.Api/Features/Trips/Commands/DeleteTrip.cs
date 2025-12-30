// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Api.Features.Trips.Commands;

/// <summary>
/// Command to delete a trip.
/// </summary>
public class DeleteTrip : IRequest<Unit>
{
    public Guid TripId { get; set; }
}

/// <summary>
/// Handler for DeleteTrip command.
/// </summary>
public class DeleteTripHandler : IRequestHandler<DeleteTrip, Unit>
{
    private readonly IFuelEconomyTrackerContext _context;

    public DeleteTripHandler(IFuelEconomyTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTrip request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            throw new KeyNotFoundException($"Trip with ID {request.TripId} not found.");
        }

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
