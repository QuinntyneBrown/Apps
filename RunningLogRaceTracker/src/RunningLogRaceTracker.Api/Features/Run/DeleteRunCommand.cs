// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.Run;

public record DeleteRunCommand(Guid RunId) : IRequest<Unit>;

public class DeleteRunCommandHandler : IRequestHandler<DeleteRunCommand, Unit>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public DeleteRunCommandHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteRunCommand request, CancellationToken cancellationToken)
    {
        var run = await _context.Runs
            .FirstOrDefaultAsync(x => x.RunId == request.RunId, cancellationToken)
            ?? throw new InvalidOperationException($"Run with ID {request.RunId} not found.");

        _context.Runs.Remove(run);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
