// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.Run;

public record GetRunsQuery() : IRequest<List<RunDto>>;

public class GetRunsQueryHandler : IRequestHandler<GetRunsQuery, List<RunDto>>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public GetRunsQueryHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<RunDto>> Handle(GetRunsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Runs
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
