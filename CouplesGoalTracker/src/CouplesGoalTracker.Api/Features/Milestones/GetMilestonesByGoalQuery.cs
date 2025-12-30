// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Milestones;

/// <summary>
/// Query to get all milestones for a goal.
/// </summary>
public class GetMilestonesByGoalQuery : IRequest<List<MilestoneDto>>
{
    /// <summary>
    /// Gets or sets the goal ID.
    /// </summary>
    public Guid GoalId { get; set; }
}

/// <summary>
/// Handler for GetMilestonesByGoalQuery.
/// </summary>
public class GetMilestonesByGoalQueryHandler : IRequestHandler<GetMilestonesByGoalQuery, List<MilestoneDto>>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMilestonesByGoalQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetMilestonesByGoalQueryHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<MilestoneDto>> Handle(GetMilestonesByGoalQuery request, CancellationToken cancellationToken)
    {
        var milestones = await _context.Milestones
            .Where(m => m.GoalId == request.GoalId)
            .OrderBy(m => m.SortOrder)
            .ToListAsync(cancellationToken);

        return milestones.Select(MilestoneDto.FromMilestone).ToList();
    }
}
