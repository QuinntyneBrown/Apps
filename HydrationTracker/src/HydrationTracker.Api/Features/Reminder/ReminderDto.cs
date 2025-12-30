// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Api.Features.Reminder;

public record ReminderDto(
    Guid ReminderId,
    Guid UserId,
    TimeSpan ReminderTime,
    string? Message,
    bool IsEnabled,
    DateTime CreatedAt);

public static class ReminderExtensions
{
    public static ReminderDto ToDto(this Core.Reminder reminder)
    {
        return new ReminderDto(
            reminder.ReminderId,
            reminder.UserId,
            reminder.ReminderTime,
            reminder.Message,
            reminder.IsEnabled,
            reminder.CreatedAt);
    }
}
