// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Core;

public record ReminderScheduledEvent
{
    public Guid ReminderId { get; init; }
    public Guid HabitId { get; init; }
    public TimeSpan ReminderTime { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
