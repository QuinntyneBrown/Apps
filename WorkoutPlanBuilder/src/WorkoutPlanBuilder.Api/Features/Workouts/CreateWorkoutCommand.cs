// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Workouts;

/// <summary>
/// Command to create a new workout.
/// </summary>
public class CreateWorkoutCommand : IRequest<WorkoutDto>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int TargetDurationMinutes { get; set; }
    public int DifficultyLevel { get; set; }
    public string? Goal { get; set; }
    public bool IsTemplate { get; set; }
    public string? ScheduledDays { get; set; }
}

/// <summary>
/// Handler for CreateWorkoutCommand.
/// </summary>
public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, WorkoutDto>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public CreateWorkoutCommandHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<WorkoutDto> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = new Workout
        {
            WorkoutId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            TargetDurationMinutes = request.TargetDurationMinutes,
            DifficultyLevel = request.DifficultyLevel,
            Goal = request.Goal,
            IsTemplate = request.IsTemplate,
            IsActive = true,
            ScheduledDays = request.ScheduledDays,
            CreatedAt = DateTime.UtcNow
        };

        _context.Workouts.Add(workout);
        await _context.SaveChangesAsync(cancellationToken);

        return workout.ToDto();
    }
}
