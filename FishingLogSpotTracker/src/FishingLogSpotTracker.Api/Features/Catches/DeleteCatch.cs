// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Catches;

/// <summary>
/// Command to delete a catch.
/// </summary>
public class DeleteCatchCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the catch ID.
    /// </summary>
    public Guid CatchId { get; set; }
}

/// <summary>
/// Handler for DeleteCatchCommand.
/// </summary>
public class DeleteCatchCommandHandler : IRequestHandler<DeleteCatchCommand, bool>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCatchCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteCatchCommandHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the DeleteCatchCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Handle(DeleteCatchCommand request, CancellationToken cancellationToken)
    {
        var catchEntity = await _context.Catches
            .FirstOrDefaultAsync(c => c.CatchId == request.CatchId, cancellationToken);

        if (catchEntity == null)
        {
            return false;
        }

        _context.Catches.Remove(catchEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
