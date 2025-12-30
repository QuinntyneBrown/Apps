// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Exercises;

/// <summary>
/// Command to update an existing exercise.
/// </summary>
public class UpdateExerciseCommand : IRequest<ExerciseDto?>
{
    public Guid ExerciseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public MuscleGroup PrimaryMuscleGroup { get; set; }
    public string? SecondaryMuscleGroups { get; set; }
    public string? Equipment { get; set; }
    public int DifficultyLevel { get; set; }
    public string? VideoUrl { get; set; }
}

/// <summary>
/// Handler for UpdateExerciseCommand.
/// </summary>
public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, ExerciseDto?>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public UpdateExerciseCommandHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<ExerciseDto?> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(e => e.ExerciseId == request.ExerciseId, cancellationToken);

        if (exercise == null)
        {
            return null;
        }

        exercise.Name = request.Name;
        exercise.Description = request.Description;
        exercise.ExerciseType = request.ExerciseType;
        exercise.PrimaryMuscleGroup = request.PrimaryMuscleGroup;
        exercise.SecondaryMuscleGroups = request.SecondaryMuscleGroups;
        exercise.Equipment = request.Equipment;
        exercise.DifficultyLevel = request.DifficultyLevel;
        exercise.VideoUrl = request.VideoUrl;

        await _context.SaveChangesAsync(cancellationToken);

        return exercise.ToDto();
    }
}
