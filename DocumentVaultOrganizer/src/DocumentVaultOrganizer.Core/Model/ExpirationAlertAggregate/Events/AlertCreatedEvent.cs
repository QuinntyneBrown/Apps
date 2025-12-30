// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Core;

public record AlertCreatedEvent
{
    public Guid ExpirationAlertId { get; init; }
    public Guid DocumentId { get; init; }
    public DateTime AlertDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
