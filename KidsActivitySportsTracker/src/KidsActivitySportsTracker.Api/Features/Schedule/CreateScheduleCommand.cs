// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Schedule;

/// <summary>
/// Command to create a new schedule.
/// </summary>
public record CreateScheduleCommand : IRequest<ScheduleDto>
{
    public Guid ActivityId { get; init; }
    public string EventType { get; init; } = string.Empty;
    public DateTime DateTime { get; init; }
    public string? Location { get; init; }
    public int? DurationMinutes { get; init; }
    public string? Notes { get; init; }
    public bool IsConfirmed { get; init; }
}

/// <summary>
/// Handler for creating a new schedule.
/// </summary>
public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, ScheduleDto>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public CreateScheduleCommandHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<ScheduleDto> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = new Core.Schedule
        {
            ScheduleId = Guid.NewGuid(),
            ActivityId = request.ActivityId,
            EventType = request.EventType,
            DateTime = request.DateTime,
            Location = request.Location,
            DurationMinutes = request.DurationMinutes,
            Notes = request.Notes,
            IsConfirmed = request.IsConfirmed,
            CreatedAt = DateTime.UtcNow
        };

        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        return schedule.ToDto();
    }
}
