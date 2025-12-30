// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Goals;

/// <summary>
/// Command to delete a goal.
/// </summary>
public class DeleteGoalCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the goal ID to delete.
    /// </summary>
    public Guid GoalId { get; set; }
}

/// <summary>
/// Handler for DeleteGoalCommand.
/// </summary>
public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand, bool>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteGoalCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteGoalCommandHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        if (goal == null)
        {
            return false;
        }

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
