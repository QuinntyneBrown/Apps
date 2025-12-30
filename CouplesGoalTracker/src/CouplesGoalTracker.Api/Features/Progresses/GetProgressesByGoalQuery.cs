// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Progresses;

/// <summary>
/// Query to get all progress entries for a goal.
/// </summary>
public class GetProgressesByGoalQuery : IRequest<List<ProgressDto>>
{
    /// <summary>
    /// Gets or sets the goal ID.
    /// </summary>
    public Guid GoalId { get; set; }
}

/// <summary>
/// Handler for GetProgressesByGoalQuery.
/// </summary>
public class GetProgressesByGoalQueryHandler : IRequestHandler<GetProgressesByGoalQuery, List<ProgressDto>>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProgressesByGoalQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetProgressesByGoalQueryHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<ProgressDto>> Handle(GetProgressesByGoalQuery request, CancellationToken cancellationToken)
    {
        var progresses = await _context.Progresses
            .Where(p => p.GoalId == request.GoalId)
            .OrderByDescending(p => p.ProgressDate)
            .ToListAsync(cancellationToken);

        return progresses.Select(ProgressDto.FromProgress).ToList();
    }
}
