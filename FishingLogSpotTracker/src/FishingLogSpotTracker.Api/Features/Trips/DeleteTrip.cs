// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Trips;

/// <summary>
/// Command to delete a trip.
/// </summary>
public class DeleteTripCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the trip ID.
    /// </summary>
    public Guid TripId { get; set; }
}

/// <summary>
/// Handler for DeleteTripCommand.
/// </summary>
public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, bool>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTripCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteTripCommandHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the DeleteTripCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            return false;
        }

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
