// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core;

public record ProjectPhaseChangedEvent
{
    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public ProjectPhase NewPhase { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
