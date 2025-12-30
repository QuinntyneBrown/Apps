// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RunningLogRaceTracker.Api.Features.TrainingPlan;

public record TrainingPlanDto(
    Guid TrainingPlanId,
    Guid UserId,
    string Name,
    Guid? RaceId,
    DateTime StartDate,
    DateTime EndDate,
    decimal? WeeklyMileageGoal,
    string? PlanDetails,
    bool IsActive,
    string? Notes,
    DateTime CreatedAt);

public static class TrainingPlanExtensions
{
    public static TrainingPlanDto ToDto(this Core.TrainingPlan trainingPlan)
    {
        return new TrainingPlanDto(
            trainingPlan.TrainingPlanId,
            trainingPlan.UserId,
            trainingPlan.Name,
            trainingPlan.RaceId,
            trainingPlan.StartDate,
            trainingPlan.EndDate,
            trainingPlan.WeeklyMileageGoal,
            trainingPlan.PlanDetails,
            trainingPlan.IsActive,
            trainingPlan.Notes,
            trainingPlan.CreatedAt);
    }
}
