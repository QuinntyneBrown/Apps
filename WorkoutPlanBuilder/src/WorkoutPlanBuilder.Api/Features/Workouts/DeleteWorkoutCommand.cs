// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Workouts;

/// <summary>
/// Command to delete a workout.
/// </summary>
public class DeleteWorkoutCommand : IRequest<bool>
{
    public Guid WorkoutId { get; set; }
}

/// <summary>
/// Handler for DeleteWorkoutCommand.
/// </summary>
public class DeleteWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand, bool>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public DeleteWorkoutCommandHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _context.Workouts
            .FirstOrDefaultAsync(w => w.WorkoutId == request.WorkoutId, cancellationToken);

        if (workout == null)
        {
            return false;
        }

        _context.Workouts.Remove(workout);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
