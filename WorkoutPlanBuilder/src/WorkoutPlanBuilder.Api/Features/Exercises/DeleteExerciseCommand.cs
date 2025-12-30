// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Exercises;

/// <summary>
/// Command to delete an exercise.
/// </summary>
public class DeleteExerciseCommand : IRequest<bool>
{
    public Guid ExerciseId { get; set; }
}

/// <summary>
/// Handler for DeleteExerciseCommand.
/// </summary>
public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, bool>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public DeleteExerciseCommandHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(e => e.ExerciseId == request.ExerciseId, cancellationToken);

        if (exercise == null)
        {
            return false;
        }

        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
