// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Schedule;

/// <summary>
/// Command to update an existing schedule.
/// </summary>
public record UpdateScheduleCommand : IRequest<ScheduleDto>
{
    public Guid ScheduleId { get; init; }
    public string EventType { get; init; } = string.Empty;
    public DateTime DateTime { get; init; }
    public string? Location { get; init; }
    public int? DurationMinutes { get; init; }
    public string? Notes { get; init; }
    public bool IsConfirmed { get; init; }
}

/// <summary>
/// Handler for updating a schedule.
/// </summary>
public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, ScheduleDto>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public UpdateScheduleCommandHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<ScheduleDto> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId, cancellationToken);

        if (schedule == null)
        {
            throw new InvalidOperationException($"Schedule with ID {request.ScheduleId} not found.");
        }

        schedule.EventType = request.EventType;
        schedule.DateTime = request.DateTime;
        schedule.Location = request.Location;
        schedule.DurationMinutes = request.DurationMinutes;
        schedule.Notes = request.Notes;
        schedule.IsConfirmed = request.IsConfirmed;

        await _context.SaveChangesAsync(cancellationToken);

        return schedule.ToDto();
    }
}
