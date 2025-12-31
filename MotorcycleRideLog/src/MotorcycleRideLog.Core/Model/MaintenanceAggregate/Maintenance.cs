// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MotorcycleRideLog.Core;

public class Maintenance
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
}
