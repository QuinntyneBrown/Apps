// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Milestones;

/// <summary>
/// Query to get a milestone by ID.
/// </summary>
public class GetMilestoneByIdQuery : IRequest<MilestoneDto?>
{
    /// <summary>
    /// Gets or sets the milestone ID.
    /// </summary>
    public Guid MilestoneId { get; set; }
}

/// <summary>
/// Handler for GetMilestoneByIdQuery.
/// </summary>
public class GetMilestoneByIdQueryHandler : IRequestHandler<GetMilestoneByIdQuery, MilestoneDto?>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMilestoneByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetMilestoneByIdQueryHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<MilestoneDto?> Handle(GetMilestoneByIdQuery request, CancellationToken cancellationToken)
    {
        var milestone = await _context.Milestones
            .FirstOrDefaultAsync(m => m.MilestoneId == request.MilestoneId, cancellationToken);

        return milestone == null ? null : MilestoneDto.FromMilestone(milestone);
    }
}
