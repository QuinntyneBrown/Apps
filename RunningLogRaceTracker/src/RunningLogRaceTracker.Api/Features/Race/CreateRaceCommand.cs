// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;

namespace RunningLogRaceTracker.Api.Features.Race;

public record CreateRaceCommand(
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

public class CreateRaceCommandHandler : IRequestHandler<CreateRaceCommand, RaceDto>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public CreateRaceCommandHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<RaceDto> Handle(CreateRaceCommand request, CancellationToken cancellationToken)
    {
        var race = new Core.Race
        {
            RaceId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            RaceType = request.RaceType,
            RaceDate = request.RaceDate,
            Location = request.Location,
            Distance = request.Distance,
            FinishTimeMinutes = request.FinishTimeMinutes,
            GoalTimeMinutes = request.GoalTimeMinutes,
            Placement = request.Placement,
            IsCompleted = request.IsCompleted,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Races.Add(race);
        await _context.SaveChangesAsync(cancellationToken);

        return race.ToDto();
    }
}
