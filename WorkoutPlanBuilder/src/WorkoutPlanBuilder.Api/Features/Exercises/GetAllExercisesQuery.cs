// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Exercises;

/// <summary>
/// Query to get all exercises.
/// </summary>
public class GetAllExercisesQuery : IRequest<List<ExerciseDto>>
{
}

/// <summary>
/// Handler for GetAllExercisesQuery.
/// </summary>
public class GetAllExercisesQueryHandler : IRequestHandler<GetAllExercisesQuery, List<ExerciseDto>>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public GetAllExercisesQueryHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<List<ExerciseDto>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Exercises
            .OrderBy(e => e.Name)
            .Select(e => e.ToDto())
            .ToListAsync(cancellationToken);
    }
}
