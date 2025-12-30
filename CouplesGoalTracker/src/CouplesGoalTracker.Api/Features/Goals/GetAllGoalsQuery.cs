// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Goals;

/// <summary>
/// Query to get all goals.
/// </summary>
public class GetAllGoalsQuery : IRequest<List<GoalDto>>
{
    /// <summary>
    /// Gets or sets the user ID to filter goals by (optional).
    /// </summary>
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for GetAllGoalsQuery.
/// </summary>
public class GetAllGoalsQueryHandler : IRequestHandler<GetAllGoalsQuery, List<GoalDto>>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllGoalsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetAllGoalsQueryHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<GoalDto>> Handle(GetAllGoalsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Goals
            .Include(g => g.Milestones)
            .AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(g => g.UserId == request.UserId.Value);
        }

        var goals = await query
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        return goals.Select(GoalDto.FromGoal).ToList();
    }
}
