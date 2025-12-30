// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Workouts;

/// <summary>
/// Command to update an existing workout.
/// </summary>
public class UpdateWorkoutCommand : IRequest<WorkoutDto?>
{
    public Guid WorkoutId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int TargetDurationMinutes { get; set; }
    public int DifficultyLevel { get; set; }
    public string? Goal { get; set; }
    public bool IsTemplate { get; set; }
    public bool IsActive { get; set; }
    public string? ScheduledDays { get; set; }
}

/// <summary>
/// Handler for UpdateWorkoutCommand.
/// </summary>
public class UpdateWorkoutCommandHandler : IRequestHandler<UpdateWorkoutCommand, WorkoutDto?>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public UpdateWorkoutCommandHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<WorkoutDto?> Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _context.Workouts
            .FirstOrDefaultAsync(w => w.WorkoutId == request.WorkoutId, cancellationToken);

        if (workout == null)
        {
            return null;
        }

        workout.Name = request.Name;
        workout.Description = request.Description;
        workout.TargetDurationMinutes = request.TargetDurationMinutes;
        workout.DifficultyLevel = request.DifficultyLevel;
        workout.Goal = request.Goal;
        workout.IsTemplate = request.IsTemplate;
        workout.IsActive = request.IsActive;
        workout.ScheduledDays = request.ScheduledDays;

        await _context.SaveChangesAsync(cancellationToken);

        return workout.ToDto();
    }
}
