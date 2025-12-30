// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.Race;

public record UpdateRaceCommand(
    Guid RaceId,
    Guid UserId,
    string Name,
    RaceType RaceType,
    DateTime RaceDate,
    string Location,
    decimal Distance,
    int? FinishTimeMinutes,
    int? GoalTimeMinutes,
    int? Placement,
    bool IsCompleted,
    string? Notes) : IRequest<RaceDto>;

public class UpdateRaceCommandHandler : IRequestHandler<UpdateRaceCommand, RaceDto>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public UpdateRaceCommandHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<RaceDto> Handle(UpdateRaceCommand request, CancellationToken cancellationToken)
    {
        var race = await _context.Races
            .FirstOrDefaultAsync(x => x.RaceId == request.RaceId, cancellationToken)
            ?? throw new InvalidOperationException($"Race with ID {request.RaceId} not found.");

        race.UserId = request.UserId;
        race.Name = request.Name;
        race.RaceType = request.RaceType;
        race.RaceDate = request.RaceDate;
        race.Location = request.Location;
        race.Distance = request.Distance;
        race.FinishTimeMinutes = request.FinishTimeMinutes;
        race.GoalTimeMinutes = request.GoalTimeMinutes;
        race.Placement = request.Placement;
        race.IsCompleted = request.IsCompleted;
        race.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return race.ToDto();
    }
}
