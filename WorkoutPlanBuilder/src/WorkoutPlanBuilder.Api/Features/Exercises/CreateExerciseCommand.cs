// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Exercises;

/// <summary>
/// Command to create a new exercise.
/// </summary>
public class CreateExerciseCommand : IRequest<ExerciseDto>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public MuscleGroup PrimaryMuscleGroup { get; set; }
    public string? SecondaryMuscleGroups { get; set; }
    public string? Equipment { get; set; }
    public int DifficultyLevel { get; set; }
    public string? VideoUrl { get; set; }
    public bool IsCustom { get; set; } = true;
}

/// <summary>
/// Handler for CreateExerciseCommand.
/// </summary>
public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, ExerciseDto>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public CreateExerciseCommandHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<ExerciseDto> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = new Exercise
        {
            ExerciseId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            ExerciseType = request.ExerciseType,
            PrimaryMuscleGroup = request.PrimaryMuscleGroup,
            SecondaryMuscleGroups = request.SecondaryMuscleGroups,
            Equipment = request.Equipment,
            DifficultyLevel = request.DifficultyLevel,
            VideoUrl = request.VideoUrl,
            IsCustom = request.IsCustom,
            CreatedAt = DateTime.UtcNow
        };

        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync(cancellationToken);

        return exercise.ToDto();
    }
}
