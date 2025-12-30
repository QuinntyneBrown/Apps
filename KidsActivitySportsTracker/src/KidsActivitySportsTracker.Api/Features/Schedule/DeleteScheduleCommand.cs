// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Schedule;

/// <summary>
/// Command to delete a schedule.
/// </summary>
public record DeleteScheduleCommand : IRequest<Unit>
{
    public Guid ScheduleId { get; init; }
}

/// <summary>
/// Handler for deleting a schedule.
/// </summary>
public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, Unit>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public DeleteScheduleCommandHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId, cancellationToken);

        if (schedule == null)
        {
            throw new InvalidOperationException($"Schedule with ID {request.ScheduleId} not found.");
        }

        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
