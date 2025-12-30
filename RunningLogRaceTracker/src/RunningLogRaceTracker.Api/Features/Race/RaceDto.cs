// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;

namespace RunningLogRaceTracker.Api.Features.Race;

public record RaceDto(
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
    string? Notes,
    DateTime CreatedAt);

public static class RaceExtensions
{
    public static RaceDto ToDto(this Core.Race race)
    {
        return new RaceDto(
            race.RaceId,
            race.UserId,
            race.Name,
            race.RaceType,
            race.RaceDate,
            race.Location,
            race.Distance,
            race.FinishTimeMinutes,
            race.GoalTimeMinutes,
            race.Placement,
            race.IsCompleted,
            race.Notes,
            race.CreatedAt);
    }
}
