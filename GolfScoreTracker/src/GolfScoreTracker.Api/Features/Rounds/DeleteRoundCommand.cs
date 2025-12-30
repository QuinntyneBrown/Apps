// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Rounds;

public class DeleteRoundCommand : IRequest<bool>
{
    public Guid RoundId { get; set; }
}

public class DeleteRoundCommandHandler : IRequestHandler<DeleteRoundCommand, bool>
{
    private readonly IGolfScoreTrackerContext _context;

    public DeleteRoundCommandHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteRoundCommand request, CancellationToken cancellationToken)
    {
        var round = await _context.Rounds
            .FirstOrDefaultAsync(r => r.RoundId == request.RoundId, cancellationToken);

        if (round == null)
        {
            return false;
        }

        _context.Rounds.Remove(round);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
