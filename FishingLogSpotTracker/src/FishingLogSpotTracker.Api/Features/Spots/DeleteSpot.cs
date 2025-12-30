// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Spots;

/// <summary>
/// Command to delete a spot.
/// </summary>
public class DeleteSpotCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the spot ID.
    /// </summary>
    public Guid SpotId { get; set; }
}

/// <summary>
/// Handler for DeleteSpotCommand.
/// </summary>
public class DeleteSpotCommandHandler : IRequestHandler<DeleteSpotCommand, bool>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSpotCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteSpotCommandHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the DeleteSpotCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Handle(DeleteSpotCommand request, CancellationToken cancellationToken)
    {
        var spot = await _context.Spots
            .FirstOrDefaultAsync(s => s.SpotId == request.SpotId, cancellationToken);

        if (spot == null)
        {
            return false;
        }

        _context.Spots.Remove(spot);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
