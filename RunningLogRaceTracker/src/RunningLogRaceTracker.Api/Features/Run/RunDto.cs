// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RunningLogRaceTracker.Api.Features.Run;

public record RunDto(
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
    int? EffortRating,
    DateTime CreatedAt);

public static class RunExtensions
{
    public static RunDto ToDto(this Core.Run run)
    {
        return new RunDto(
            run.RunId,
            run.UserId,
            run.Distance,
            run.DurationMinutes,
            run.CompletedAt,
            run.AveragePace,
            run.AverageHeartRate,
            run.ElevationGain,
            run.CaloriesBurned,
            run.Route,
            run.Weather,
            run.Notes,
            run.EffortRating,
            run.CreatedAt);
    }
}
