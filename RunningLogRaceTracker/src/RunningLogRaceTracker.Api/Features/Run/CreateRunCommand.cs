// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;

namespace RunningLogRaceTracker.Api.Features.Run;

public record CreateRunCommand(
    Guid UserId,
    decimal Distance,
    int DurationMinutes,
    DateTime CompletedAt,
    decimal? AveragePace,
    int? AverageHeartRate,
    int? ElevationGain,
    int? CaloriesBurned,
    string? Route,
    string? Weather,
    string? Notes,
    int? EffortRating) : IRequest<RunDto>;

public class CreateRunCommandHandler : IRequestHandler<CreateRunCommand, RunDto>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public CreateRunCommandHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<RunDto> Handle(CreateRunCommand request, CancellationToken cancellationToken)
    {
        var run = new Core.Run
        {
            RunId = Guid.NewGuid(),
            UserId = request.UserId,
            Distance = request.Distance,
            DurationMinutes = request.DurationMinutes,
            CompletedAt = request.CompletedAt,
            AveragePace = request.AveragePace,
            AverageHeartRate = request.AverageHeartRate,
            ElevationGain = request.ElevationGain,
            CaloriesBurned = request.CaloriesBurned,
            Route = request.Route,
            Weather = request.Weather,
            Notes = request.Notes,
            EffortRating = request.EffortRating,
            CreatedAt = DateTime.UtcNow
        };

        _context.Runs.Add(run);
        await _context.SaveChangesAsync(cancellationToken);

        return run.ToDto();
    }
}
