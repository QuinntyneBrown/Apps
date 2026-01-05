// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core;

public record SessionCreatedEvent
{
    public Guid SessionId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public SessionType SessionType { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
