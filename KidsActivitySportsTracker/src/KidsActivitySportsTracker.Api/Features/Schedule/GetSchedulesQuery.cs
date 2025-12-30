// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Schedule;

/// <summary>
/// Query to get all schedules.
/// </summary>
public record GetSchedulesQuery : IRequest<List<ScheduleDto>>
{
    public Guid? ActivityId { get; init; }
}

/// <summary>
/// Handler for getting all schedules.
/// </summary>
public class GetSchedulesQueryHandler : IRequestHandler<GetSchedulesQuery, List<ScheduleDto>>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public GetSchedulesQueryHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<ScheduleDto>> Handle(GetSchedulesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Schedules.AsQueryable();

        if (request.ActivityId.HasValue)
        {
            query = query.Where(s => s.ActivityId == request.ActivityId.Value);
        }

        var schedules = await query
            .OrderBy(s => s.DateTime)
            .ToListAsync(cancellationToken);

        return schedules.Select(s => s.ToDto()).ToList();
    }
}
