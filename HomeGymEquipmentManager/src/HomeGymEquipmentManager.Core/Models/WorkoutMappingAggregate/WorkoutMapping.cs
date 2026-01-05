// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core;

public class WorkoutMapping
{
    public Guid WorkoutMappingId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid EquipmentId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public string? MuscleGroup { get; set; }
    public string? Instructions { get; set; }
    public bool IsFavorite { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
    }
}
