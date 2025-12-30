// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core;

/// <summary>
/// Represents a dose schedule for taking medication.
/// </summary>
public class DoseSchedule
{
    /// <summary>
    /// Gets or sets the unique identifier for the dose schedule.
    /// </summary>
    public Guid DoseScheduleId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this dose schedule.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the medication ID.
    /// </summary>
    public Guid MedicationId { get; set; }

    /// <summary>
    /// Gets or sets the scheduled time to take the dose.
    /// </summary>
    public TimeSpan ScheduledTime { get; set; }

    /// <summary>
    /// Gets or sets the days of week (JSON array or comma-separated).
    /// </summary>
    public string DaysOfWeek { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the frequency (e.g., Daily, Weekly, As Needed).
    /// </summary>
    public string Frequency { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether reminders are enabled.
    /// </summary>
    public bool ReminderEnabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the reminder time offset in minutes before scheduled time.
    /// </summary>
    public int ReminderOffsetMinutes { get; set; } = 0;

    /// <summary>
    /// Gets or sets the timestamp when the dose was last taken.
    /// </summary>
    public DateTime? LastTaken { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the schedule is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the associated medication.
    /// </summary>
    public Medication? Medication { get; set; }

    /// <summary>
    /// Marks the dose as taken at the current time.
    /// </summary>
    public void MarkAsTaken()
    {
        LastTaken = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the dose was taken today.
    /// </summary>
    /// <returns>True if taken today; otherwise, false.</returns>
    public bool WasTakenToday()
    {
        return LastTaken.HasValue && LastTaken.Value.Date == DateTime.UtcNow.Date;
    }
}
