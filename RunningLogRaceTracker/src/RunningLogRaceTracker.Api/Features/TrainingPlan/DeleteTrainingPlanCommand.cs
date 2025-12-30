// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.TrainingPlan;

public record DeleteTrainingPlanCommand(Guid TrainingPlanId) : IRequest<Unit>;

public class DeleteTrainingPlanCommandHandler : IRequestHandler<DeleteTrainingPlanCommand, Unit>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public DeleteTrainingPlanCommandHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTrainingPlanCommand request, CancellationToken cancellationToken)
    {
        var trainingPlan = await _context.TrainingPlans
            .FirstOrDefaultAsync(x => x.TrainingPlanId == request.TrainingPlanId, cancellationToken)
            ?? throw new InvalidOperationException($"TrainingPlan with ID {request.TrainingPlanId} not found.");

        _context.TrainingPlans.Remove(trainingPlan);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
