// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core;

public class Equipment
{
    public Guid EquipmentId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public EquipmentType EquipmentType { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Maintenance> MaintenanceRecords { get; set; } = new List<Maintenance>();
    
    public bool RequiresMaintenance()
    {
        return EquipmentType == EquipmentType.Cardio || EquipmentType == EquipmentType.Strength;
    }
}
