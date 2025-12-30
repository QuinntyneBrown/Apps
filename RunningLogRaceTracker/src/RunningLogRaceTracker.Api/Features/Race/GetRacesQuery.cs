// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.Race;

public record GetRacesQuery() : IRequest<List<RaceDto>>;

public class GetRacesQueryHandler : IRequestHandler<GetRacesQuery, List<RaceDto>>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public GetRacesQueryHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<RaceDto>> Handle(GetRacesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Races
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
