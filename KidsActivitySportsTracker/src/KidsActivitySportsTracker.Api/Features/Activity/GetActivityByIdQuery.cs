// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Activity;

/// <summary>
/// Query to get an activity by ID.
/// </summary>
public record GetActivityByIdQuery : IRequest<ActivityDto?>
{
    public Guid ActivityId { get; init; }
}

/// <summary>
/// Handler for getting an activity by ID.
/// </summary>
public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ActivityDto?>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public GetActivityByIdQueryHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<ActivityDto?> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
    {
        var activity = await _context.Activities
            .FirstOrDefaultAsync(a => a.ActivityId == request.ActivityId, cancellationToken);

        return activity?.ToDto();
    }
}
