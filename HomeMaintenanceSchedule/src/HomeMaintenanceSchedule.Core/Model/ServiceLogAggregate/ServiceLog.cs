// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Represents a service log entry for a maintenance task.
/// </summary>
public class ServiceLog
{
    /// <summary>
    /// Gets or sets the unique identifier for the service log.
    /// </summary>
    public Guid ServiceLogId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the maintenance task ID.
    /// </summary>
    public Guid MaintenanceTaskId { get; set; }

    /// <summary>
    /// Gets or sets the maintenance task.
    /// </summary>
    public MaintenanceTask? MaintenanceTask { get; set; }

    /// <summary>
    /// Gets or sets the service date.
    /// </summary>
    public DateTime ServiceDate { get; set; }

    /// <summary>
    /// Gets or sets the description of the service performed.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the contractor ID who performed the service.
    /// </summary>
    public Guid? ContractorId { get; set; }

    /// <summary>
    /// Gets or sets the contractor who performed the service.
    /// </summary>
    public Contractor? Contractor { get; set; }

    /// <summary>
    /// Gets or sets the cost of the service.
    /// </summary>
    public decimal? Cost { get; set; }

    /// <summary>
    /// Gets or sets notes about the service.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the parts used during service.
    /// </summary>
    public string? PartsUsed { get; set; }

    /// <summary>
    /// Gets or sets the labor hours spent.
    /// </summary>
    public decimal? LaborHours { get; set; }

    /// <summary>
    /// Gets or sets the warranty expiration date if applicable.
    /// </summary>
    public DateTime? WarrantyExpiresAt { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
