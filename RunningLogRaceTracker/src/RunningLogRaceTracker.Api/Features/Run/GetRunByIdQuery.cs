// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.Run;

public record GetRunByIdQuery(Guid RunId) : IRequest<RunDto>;

public class GetRunByIdQueryHandler : IRequestHandler<GetRunByIdQuery, RunDto>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public GetRunByIdQueryHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<RunDto> Handle(GetRunByIdQuery request, CancellationToken cancellationToken)
    {
        var run = await _context.Runs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RunId == request.RunId, cancellationToken)
            ?? throw new InvalidOperationException($"Run with ID {request.RunId} not found.");

        return run.ToDto();
    }
}
