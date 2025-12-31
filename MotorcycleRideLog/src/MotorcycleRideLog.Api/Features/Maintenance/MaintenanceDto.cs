// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;

namespace MotorcycleRideLog.Api.Features.Maintenance;

public class MaintenanceDto
{
    public Guid MaintenanceId { get; set; }
    public Guid UserId { get; set; }
    public Guid MotorcycleId { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public MaintenanceType Type { get; set; }
    public int? MileageAtMaintenance { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal? Cost { get; set; }
    public string? ServiceProvider { get; set; }
    public string? PartsReplaced { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }

    public static MaintenanceDto FromEntity(Core.Maintenance maintenance)
    {
        return new MaintenanceDto
        {
            MaintenanceId = maintenance.MaintenanceId,
            UserId = maintenance.UserId,
            MotorcycleId = maintenance.MotorcycleId,
            MaintenanceDate = maintenance.MaintenanceDate,
            Type = maintenance.Type,
            MileageAtMaintenance = maintenance.MileageAtMaintenance,
            Description = maintenance.Description,
            Cost = maintenance.Cost,
            ServiceProvider = maintenance.ServiceProvider,
            PartsReplaced = maintenance.PartsReplaced,
            Notes = maintenance.Notes,
            CreatedAt = maintenance.CreatedAt
        };
    }
}
