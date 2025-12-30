// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core;

public record ApplianceAddedEvent
{
    public Guid ApplianceId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
