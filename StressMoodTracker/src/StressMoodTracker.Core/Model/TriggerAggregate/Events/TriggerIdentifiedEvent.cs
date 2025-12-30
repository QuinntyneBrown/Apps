// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace StressMoodTracker.Core;

public record TriggerIdentifiedEvent
{
    public Guid TriggerId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
