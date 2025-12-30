// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core;

public record EquipmentAddedEvent
{
    public Guid EquipmentId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public EquipmentType EquipmentType { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
