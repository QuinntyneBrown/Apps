// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Goals;

/// <summary>
/// Command to create a new goal.
/// </summary>
public class CreateGoalCommand : IRequest<GoalDto>
{
    /// <summary>
    /// Gets or sets the user ID who is creating this goal.
    /// </summary>
    public Guid UserId { get; set; }

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
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets the priority level (1-5, with 5 being highest).
    /// </summary>
    public int Priority { get; set; } = 3;

    /// <summary>
    /// Gets or sets a value indicating whether this goal is shared with partner.
    /// </summary>
    public bool IsShared { get; set; } = true;
}

/// <summary>
/// Handler for CreateGoalCommand.
/// </summary>
public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, GoalDto>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateGoalCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateGoalCommandHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<GoalDto> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            Status = GoalStatus.NotStarted,
            TargetDate = request.TargetDate,
            Priority = request.Priority,
            IsShared = request.IsShared,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        return GoalDto.FromGoal(goal);
    }
}
