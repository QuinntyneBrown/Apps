// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;

namespace HomeGymEquipmentManager.Api.Features.Equipment;

public class EquipmentDto
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
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    public static EquipmentDto FromEntity(Core.Equipment equipment)
    {
        return new EquipmentDto
        {
            EquipmentId = equipment.EquipmentId,
            UserId = equipment.UserId,
            Name = equipment.Name,
            EquipmentType = equipment.EquipmentType,
            Brand = equipment.Brand,
            Model = equipment.Model,
            PurchaseDate = equipment.PurchaseDate,
            PurchasePrice = equipment.PurchasePrice,
            Location = equipment.Location,
            Notes = equipment.Notes,
            IsActive = equipment.IsActive,
            CreatedAt = equipment.CreatedAt
        };
    }
}
