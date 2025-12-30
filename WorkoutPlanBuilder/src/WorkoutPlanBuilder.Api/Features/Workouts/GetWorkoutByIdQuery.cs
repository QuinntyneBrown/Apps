// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Workouts;

/// <summary>
/// Query to get a workout by ID.
/// </summary>
public class GetWorkoutByIdQuery : IRequest<WorkoutDto?>
{
    public Guid WorkoutId { get; set; }
}

/// <summary>
/// Handler for GetWorkoutByIdQuery.
/// </summary>
public class GetWorkoutByIdQueryHandler : IRequestHandler<GetWorkoutByIdQuery, WorkoutDto?>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public GetWorkoutByIdQueryHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<WorkoutDto?> Handle(GetWorkoutByIdQuery request, CancellationToken cancellationToken)
    {
        var workout = await _context.Workouts
            .FirstOrDefaultAsync(w => w.WorkoutId == request.WorkoutId, cancellationToken);

        return workout?.ToDto();
    }
}
