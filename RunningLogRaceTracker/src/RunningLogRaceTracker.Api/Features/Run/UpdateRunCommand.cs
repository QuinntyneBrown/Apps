// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.Run;

public record UpdateRunCommand(
    Guid RunId,
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

public class UpdateRunCommandHandler : IRequestHandler<UpdateRunCommand, RunDto>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public UpdateRunCommandHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<RunDto> Handle(UpdateRunCommand request, CancellationToken cancellationToken)
    {
        var run = await _context.Runs
            .FirstOrDefaultAsync(x => x.RunId == request.RunId, cancellationToken)
            ?? throw new InvalidOperationException($"Run with ID {request.RunId} not found.");

        run.UserId = request.UserId;
        run.Distance = request.Distance;
        run.DurationMinutes = request.DurationMinutes;
        run.CompletedAt = request.CompletedAt;
        run.AveragePace = request.AveragePace;
        run.AverageHeartRate = request.AverageHeartRate;
        run.ElevationGain = request.ElevationGain;
        run.CaloriesBurned = request.CaloriesBurned;
        run.Route = request.Route;
        run.Weather = request.Weather;
        run.Notes = request.Notes;
        run.EffortRating = request.EffortRating;

        await _context.SaveChangesAsync(cancellationToken);

        return run.ToDto();
    }
}
