// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core;

public record PhotoRatedEvent
{
    public Guid PhotoId { get; init; }
    public Guid UserId { get; init; }
    public int Rating { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
