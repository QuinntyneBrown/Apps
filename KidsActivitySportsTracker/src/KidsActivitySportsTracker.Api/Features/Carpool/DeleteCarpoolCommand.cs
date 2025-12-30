// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Carpool;

/// <summary>
/// Command to delete a carpool.
/// </summary>
public record DeleteCarpoolCommand : IRequest<Unit>
{
    public Guid CarpoolId { get; init; }
}

/// <summary>
/// Handler for deleting a carpool.
/// </summary>
public class DeleteCarpoolCommandHandler : IRequestHandler<DeleteCarpoolCommand, Unit>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public DeleteCarpoolCommandHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCarpoolCommand request, CancellationToken cancellationToken)
    {
        var carpool = await _context.Carpools
            .FirstOrDefaultAsync(c => c.CarpoolId == request.CarpoolId, cancellationToken);

        if (carpool == null)
        {
            throw new InvalidOperationException($"Carpool with ID {request.CarpoolId} not found.");
        }

        _context.Carpools.Remove(carpool);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
