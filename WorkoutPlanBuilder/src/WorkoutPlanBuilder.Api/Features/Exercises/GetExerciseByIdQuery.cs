// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Exercises;

/// <summary>
/// Query to get an exercise by ID.
/// </summary>
public class GetExerciseByIdQuery : IRequest<ExerciseDto?>
{
    public Guid ExerciseId { get; set; }
}

/// <summary>
/// Handler for GetExerciseByIdQuery.
/// </summary>
public class GetExerciseByIdQueryHandler : IRequestHandler<GetExerciseByIdQuery, ExerciseDto?>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public GetExerciseByIdQueryHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<ExerciseDto?> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
    {
        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(e => e.ExerciseId == request.ExerciseId, cancellationToken);

        return exercise?.ToDto();
    }
}
