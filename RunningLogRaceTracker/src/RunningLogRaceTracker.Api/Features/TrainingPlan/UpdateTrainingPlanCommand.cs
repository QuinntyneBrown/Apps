// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.TrainingPlan;

public record UpdateTrainingPlanCommand(
    Guid TrainingPlanId,
    Guid UserId,
    string Name,
    Guid? RaceId,
    DateTime StartDate,
    DateTime EndDate,
    decimal? WeeklyMileageGoal,
    string? PlanDetails,
    bool IsActive,
    string? Notes) : IRequest<TrainingPlanDto>;

public class UpdateTrainingPlanCommandHandler : IRequestHandler<UpdateTrainingPlanCommand, TrainingPlanDto>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public UpdateTrainingPlanCommandHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<TrainingPlanDto> Handle(UpdateTrainingPlanCommand request, CancellationToken cancellationToken)
    {
        var trainingPlan = await _context.TrainingPlans
            .FirstOrDefaultAsync(x => x.TrainingPlanId == request.TrainingPlanId, cancellationToken)
            ?? throw new InvalidOperationException($"TrainingPlan with ID {request.TrainingPlanId} not found.");

        trainingPlan.UserId = request.UserId;
        trainingPlan.Name = request.Name;
        trainingPlan.RaceId = request.RaceId;
        trainingPlan.StartDate = request.StartDate;
        trainingPlan.EndDate = request.EndDate;
        trainingPlan.WeeklyMileageGoal = request.WeeklyMileageGoal;
        trainingPlan.PlanDetails = request.PlanDetails;
        trainingPlan.IsActive = request.IsActive;
        trainingPlan.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return trainingPlan.ToDto();
    }
}
