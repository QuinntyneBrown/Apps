// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.TrainingPlan;

public record GetTrainingPlanByIdQuery(Guid TrainingPlanId) : IRequest<TrainingPlanDto>;

public class GetTrainingPlanByIdQueryHandler : IRequestHandler<GetTrainingPlanByIdQuery, TrainingPlanDto>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public GetTrainingPlanByIdQueryHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<TrainingPlanDto> Handle(GetTrainingPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var trainingPlan = await _context.TrainingPlans
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TrainingPlanId == request.TrainingPlanId, cancellationToken)
            ?? throw new InvalidOperationException($"TrainingPlan with ID {request.TrainingPlanId} not found.");

        return trainingPlan.ToDto();
    }
}
