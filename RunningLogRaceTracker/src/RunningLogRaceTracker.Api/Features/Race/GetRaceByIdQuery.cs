// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.Race;

public record GetRaceByIdQuery(Guid RaceId) : IRequest<RaceDto>;

public class GetRaceByIdQueryHandler : IRequestHandler<GetRaceByIdQuery, RaceDto>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public GetRaceByIdQueryHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<RaceDto> Handle(GetRaceByIdQuery request, CancellationToken cancellationToken)
    {
        var race = await _context.Races
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RaceId == request.RaceId, cancellationToken)
            ?? throw new InvalidOperationException($"Race with ID {request.RaceId} not found.");

        return race.ToDto();
    }
}
