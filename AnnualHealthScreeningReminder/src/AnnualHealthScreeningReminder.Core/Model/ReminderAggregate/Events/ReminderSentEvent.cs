// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core;

public record ReminderSentEvent
{
    public Guid ReminderId { get; init; }
    public Guid ScreeningId { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
