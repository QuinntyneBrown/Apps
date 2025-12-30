// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core;

public record MaintenancePerformedEvent
{
    public Guid MaintenanceId { get; init; }
    public Guid EquipmentId { get; init; }
    public DateTime MaintenanceDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
