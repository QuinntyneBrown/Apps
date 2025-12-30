// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RunningLogRaceTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace RunningLogRaceTracker.Api.Features.TrainingPlan;

public record GetTrainingPlansQuery() : IRequest<List<TrainingPlanDto>>;

public class GetTrainingPlansQueryHandler : IRequestHandler<GetTrainingPlansQuery, List<TrainingPlanDto>>
{
    private readonly IRunningLogRaceTrackerContext _context;

    public GetTrainingPlansQueryHandler(IRunningLogRaceTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<TrainingPlanDto>> Handle(GetTrainingPlansQuery request, CancellationToken cancellationToken)
    {
        return await _context.TrainingPlans
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
