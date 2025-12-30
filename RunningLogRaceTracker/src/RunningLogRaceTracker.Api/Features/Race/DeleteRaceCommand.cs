// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.Race;

public record DeleteRaceCommand(Guid RaceId) : IRequest<Unit>;

public class DeleteRaceCommandHandler : IRequestHandler<DeleteRaceCommand, Unit>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public DeleteRaceCommandHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteRaceCommand request, CancellationToken cancellationToken)
    {
        var race = await _context.Races
            .FirstOrDefaultAsync(x => x.RaceId == request.RaceId, cancellationToken)
            ?? throw new InvalidOperationException($"Race with ID {request.RaceId} not found.");

        _context.Races.Remove(race);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
