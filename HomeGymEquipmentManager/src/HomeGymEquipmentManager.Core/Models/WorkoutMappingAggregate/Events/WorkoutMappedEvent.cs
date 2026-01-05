// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core;

public record WorkoutMappedEvent
{
    public Guid WorkoutMappingId { get; init; }
    public Guid EquipmentId { get; init; }
    public string ExerciseName { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
