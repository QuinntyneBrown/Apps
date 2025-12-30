// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Goals;

/// <summary>
/// Query to get a goal by ID.
/// </summary>
public class GetGoalByIdQuery : IRequest<GoalDto?>
{
    /// <summary>
    /// Gets or sets the goal ID.
    /// </summary>
    public Guid GoalId { get; set; }
}

/// <summary>
/// Handler for GetGoalByIdQuery.
/// </summary>
public class GetGoalByIdQueryHandler : IRequestHandler<GetGoalByIdQuery, GoalDto?>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetGoalByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetGoalByIdQueryHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<GoalDto?> Handle(GetGoalByIdQuery request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .Include(g => g.Milestones)
            .Include(g => g.Progresses)
            .FirstOrDefaultAsync(g => g.GoalId == request.GoalId, cancellationToken);

        return goal == null ? null : GoalDto.FromGoal(goal);
    }
}
