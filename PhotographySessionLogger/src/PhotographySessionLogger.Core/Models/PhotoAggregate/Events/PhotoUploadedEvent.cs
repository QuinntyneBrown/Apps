// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core;

public record PhotoUploadedEvent
{
    public Guid PhotoId { get; init; }
    public Guid UserId { get; init; }
    public Guid SessionId { get; init; }
    public string FileName { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
