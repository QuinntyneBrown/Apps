// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Api.Features.Schedule;

/// <summary>
/// Data transfer object for Schedule.
/// </summary>
public record ScheduleDto
{
    public Guid ScheduleId { get; init; }
    public Guid ActivityId { get; init; }
    public string EventType { get; init; } = string.Empty;
    public DateTime DateTime { get; init; }
    public string? Location { get; init; }
    public int? DurationMinutes { get; init; }
    public string? Notes { get; init; }
    public bool IsConfirmed { get; init; }
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Schedule entity.
/// </summary>
public static class ScheduleExtensions
{
    /// <summary>
    /// Converts a Schedule entity to ScheduleDto.
    /// </summary>
    public static ScheduleDto ToDto(this Core.Schedule schedule)
    {
        return new ScheduleDto
        {
            ScheduleId = schedule.ScheduleId,
            ActivityId = schedule.ActivityId,
            EventType = schedule.EventType,
            DateTime = schedule.DateTime,
            Location = schedule.Location,
            DurationMinutes = schedule.DurationMinutes,
            Notes = schedule.Notes,
            IsConfirmed = schedule.IsConfirmed,
            CreatedAt = schedule.CreatedAt
        };
    }
}
