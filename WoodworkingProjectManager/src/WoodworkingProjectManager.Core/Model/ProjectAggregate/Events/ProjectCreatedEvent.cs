// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WoodworkingProjectManager.Core;

public record ProjectCreatedEvent
{
    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public ProjectStatus Status { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
