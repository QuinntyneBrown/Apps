// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Core;

public record HabitCreatedEvent
{
    public Guid HabitId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public HabitFrequency Frequency { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
