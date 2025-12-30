// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Api.Features.Maintenance;

public class MaintenanceDto
{
    public Guid MaintenanceId { get; set; }
    public Guid UserId { get; set; }
    public Guid EquipmentId { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal? Cost { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }

    public static MaintenanceDto FromEntity(Core.Maintenance maintenance)
    {
        return new MaintenanceDto
        {
            MaintenanceId = maintenance.MaintenanceId,
            UserId = maintenance.UserId,
            EquipmentId = maintenance.EquipmentId,
            MaintenanceDate = maintenance.MaintenanceDate,
            Description = maintenance.Description,
            Cost = maintenance.Cost,
            NextMaintenanceDate = maintenance.NextMaintenanceDate,
            Notes = maintenance.Notes,
            CreatedAt = maintenance.CreatedAt
        };
    }
}
