// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core;

public record ProjectStartedEvent
{
    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public string CarMake { get; init; } = string.Empty;
    public string CarModel { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
