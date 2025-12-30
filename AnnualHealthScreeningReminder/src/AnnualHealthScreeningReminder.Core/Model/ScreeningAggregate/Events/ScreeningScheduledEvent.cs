// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core;

public record ScreeningScheduledEvent
{
    public Guid ScreeningId { get; init; }
    public Guid UserId { get; init; }
    public ScreeningType ScreeningType { get; init; }
    public DateTime? NextDueDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
