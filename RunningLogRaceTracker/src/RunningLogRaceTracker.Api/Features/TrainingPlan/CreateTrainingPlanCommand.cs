// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;

namespace RunningLogRaceTracker.Api.Features.TrainingPlan;

public record CreateTrainingPlanCommand(
    Guid UserId,
    string Name,
    Guid? RaceId,
    DateTime StartDate,
    DateTime EndDate,
    decimal? WeeklyMileageGoal,
    string? PlanDetails,
    bool IsActive,
    string? Notes) : IRequest<TrainingPlanDto>;

public class CreateTrainingPlanCommandHandler : IRequestHandler<CreateTrainingPlanCommand, TrainingPlanDto>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public CreateTrainingPlanCommandHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<TrainingPlanDto> Handle(CreateTrainingPlanCommand request, CancellationToken cancellationToken)
    {
        var trainingPlan = new Core.TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            RaceId = request.RaceId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            WeeklyMileageGoal = request.WeeklyMileageGoal,
            PlanDetails = request.PlanDetails,
            IsActive = request.IsActive,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.TrainingPlans.Add(trainingPlan);
        await _context.SaveChangesAsync(cancellationToken);

        return trainingPlan.ToDto();
    }
}
