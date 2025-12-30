// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.HoleScores;

public class DeleteHoleScoreCommand : IRequest<bool>
{
    public Guid HoleScoreId { get; set; }
}

public class DeleteHoleScoreCommandHandler : IRequestHandler<DeleteHoleScoreCommand, bool>
{
    private readonly IGolfScoreTrackerContext _context;

    public DeleteHoleScoreCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteHoleScoreCommand request, CancellationToken cancellationToken)
    {
        var holeScore = await _context.HoleScores
            .FirstOrDefaultAsync(h => h.HoleScoreId == request.HoleScoreId, cancellationToken);

        if (holeScore == null)
        {
            return false;
        }

        _context.HoleScores.Remove(holeScore);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
