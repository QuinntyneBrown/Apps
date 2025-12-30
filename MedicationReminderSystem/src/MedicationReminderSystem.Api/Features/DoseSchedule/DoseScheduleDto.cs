// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Api.Features.DoseSchedule;

/// <summary>
/// Data transfer object for DoseSchedule.
/// </summary>
public record DoseScheduleDto
{
    public Guid DoseScheduleId { get; init; }
    public Guid UserId { get; init; }
    public Guid MedicationId { get; init; }
    public TimeSpan ScheduledTime { get; init; }
    public string DaysOfWeek { get; init; } = string.Empty;
    public string Frequency { get; init; } = string.Empty;
    public bool ReminderEnabled { get; init; }
    public int ReminderOffsetMinutes { get; init; }
    public DateTime? LastTaken { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for DoseSchedule entity.
/// </summary>
public static class DoseScheduleExtensions
{
    /// <summary>
    /// Converts a DoseSchedule entity to a DoseScheduleDto.
    /// </summary>
    public static DoseScheduleDto ToDto(this Core.DoseSchedule doseSchedule)
    {
        return new DoseScheduleDto
        {
            DoseScheduleId = doseSchedule.DoseScheduleId,
            UserId = doseSchedule.UserId,
            MedicationId = doseSchedule.MedicationId,
            ScheduledTime = doseSchedule.ScheduledTime,
            DaysOfWeek = doseSchedule.DaysOfWeek,
            Frequency = doseSchedule.Frequency,
            ReminderEnabled = doseSchedule.ReminderEnabled,
            ReminderOffsetMinutes = doseSchedule.ReminderOffsetMinutes,
            LastTaken = doseSchedule.LastTaken,
            IsActive = doseSchedule.IsActive,
            CreatedAt = doseSchedule.CreatedAt
        };
    }
}
