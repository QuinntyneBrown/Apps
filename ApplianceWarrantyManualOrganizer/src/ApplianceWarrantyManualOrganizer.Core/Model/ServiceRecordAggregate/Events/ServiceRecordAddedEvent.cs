// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core;

public record ServiceRecordAddedEvent
{
    public Guid ServiceRecordId { get; init; }
    public Guid ApplianceId { get; init; }
    public DateTime ServiceDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
