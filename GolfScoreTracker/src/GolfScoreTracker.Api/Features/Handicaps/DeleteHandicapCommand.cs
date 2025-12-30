// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Handicaps;

public class DeleteHandicapCommand : IRequest<bool>
{
    public Guid HandicapId { get; set; }
}

public class DeleteHandicapCommandHandler : IRequestHandler<DeleteHandicapCommand, bool>
{
    private readonly IGolfScoreTrackerContext _context;

    public DeleteHandicapCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteHandicapCommand request, CancellationToken cancellationToken)
    {
        var handicap = await _context.Handicaps
            .FirstOrDefaultAsync(h => h.HandicapId == request.HandicapId, cancellationToken);

        if (handicap == null)
        {
            return false;
        }

        _context.Handicaps.Remove(handicap);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
