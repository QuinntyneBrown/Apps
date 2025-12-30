// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Api.Features.Goal;

public record GoalDto(
    Guid GoalId,
    Guid UserId,
    decimal DailyGoalMl,
    DateTime StartDate,
    bool IsActive,
    string? Notes,
    DateTime CreatedAt);

public static class GoalExtensions
{
    public static GoalDto ToDto(this Core.Goal goal)
    {
        return new GoalDto(
            goal.GoalId,
            goal.UserId,
            goal.DailyGoalMl,
            goal.StartDate,
            goal.IsActive,
            goal.Notes,
            goal.CreatedAt);
    }
}
