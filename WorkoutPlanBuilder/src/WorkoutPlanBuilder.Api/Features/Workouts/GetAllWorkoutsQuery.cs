// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Workouts;

/// <summary>
/// Query to get all workouts.
/// </summary>
public class GetAllWorkoutsQuery : IRequest<List<WorkoutDto>>
{
}

/// <summary>
/// Handler for GetAllWorkoutsQuery.
/// </summary>
public class GetAllWorkoutsQueryHandler : IRequestHandler<GetAllWorkoutsQuery, List<WorkoutDto>>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public GetAllWorkoutsQueryHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<List<WorkoutDto>> Handle(GetAllWorkoutsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Workouts
            .OrderByDescending(w => w.CreatedAt)
            .Select(w => w.ToDto())
            .ToListAsync(cancellationToken);
    }
}
