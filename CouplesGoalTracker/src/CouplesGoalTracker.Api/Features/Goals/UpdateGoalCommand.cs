// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Goals;

/// <summary>
/// Command to update an existing goal.
/// </summary>
public class UpdateGoalCommand : IRequest<GoalDto?>
{
    /// <summary>
    /// Gets or sets the goal ID to update.
    /// </summary>
    public Guid GoalId { get; set; }

    /// <summary>
    /// Gets or sets the title of the goal.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the goal.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the goal.
    /// </summary>
    public GoalCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the status of the goal.
    /// </summary>
    public GoalStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets the priority level (1-5, with 5 being highest).
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this goal is shared with partner.
    /// </summary>
    public bool IsShared { get; set; }
}

/// <summary>
/// Handler for UpdateGoalCommand.
/// </summary>
public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, GoalDto?>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateGoalCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateGoalCommandHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<GoalDto?> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .Include(g => g.Milestones)
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        if (goal == null)
        {
            return null;
        }

        goal.Title = request.Title;
        goal.Description = request.Description;
        goal.Category = request.Category;
        goal.Status = request.Status;
        goal.TargetDate = request.TargetDate;
        goal.Priority = request.Priority;
        goal.IsShared = request.IsShared;
        goal.UpdatedAt = DateTime.UtcNow;

        if (request.Status == GoalStatus.Completed && goal.CompletedDate == null)
        {
            goal.CompletedDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return GoalDto.FromGoal(goal);
    }
}
