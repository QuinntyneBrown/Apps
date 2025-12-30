// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Activity;

/// <summary>
/// Query to get all activities.
/// </summary>
public record GetActivitiesQuery : IRequest<List<ActivityDto>>
{
    public Guid? UserId { get; init; }
}

/// <summary>
/// Handler for getting all activities.
/// </summary>
public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, List<ActivityDto>>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public GetActivitiesQueryHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<ActivityDto>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Activities.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(a => a.UserId == request.UserId.Value);
        }

        var activities = await query
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return activities.Select(a => a.ToDto()).ToList();
    }
}
