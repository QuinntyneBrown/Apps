// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WoodworkingProjectManager.Core;

public record ProjectCompletedEvent
{
    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public DateTime CompletionDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
