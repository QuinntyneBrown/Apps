// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Schedule;

/// <summary>
/// Query to get a schedule by ID.
/// </summary>
public record GetScheduleByIdQuery : IRequest<ScheduleDto?>
{
    public Guid ScheduleId { get; init; }
}

/// <summary>
/// Handler for getting a schedule by ID.
/// </summary>
public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, ScheduleDto?>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public GetScheduleByIdQueryHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<ScheduleDto?> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId, cancellationToken);

        return schedule?.ToDto();
    }
}
