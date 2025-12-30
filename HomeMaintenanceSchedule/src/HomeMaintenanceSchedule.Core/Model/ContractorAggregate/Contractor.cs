// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Represents a contractor or service provider.
/// </summary>
public class Contractor
{
    /// <summary>
    /// Gets or sets the unique identifier for the contractor.
    /// </summary>
    public Guid ContractorId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this contractor.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the contractor's name or company name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the specialty or trade.
    /// </summary>
    public string? Specialty { get; set; }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the website.
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the license number.
    /// </summary>
    public string? LicenseNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the contractor is insured.
    /// </summary>
    public bool IsInsured { get; set; }

    /// <summary>
    /// Gets or sets the rating (1-5 stars).
    /// </summary>
    public int? Rating { get; set; }

    /// <summary>
    /// Gets or sets notes about the contractor.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the contractor is active/preferred.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of maintenance tasks assigned to this contractor.
    /// </summary>
    public ICollection<MaintenanceTask> MaintenanceTasks { get; set; } = new List<MaintenanceTask>();

    /// <summary>
    /// Gets or sets the collection of service logs for this contractor.
    /// </summary>
    public ICollection<ServiceLog> ServiceLogs { get; set; } = new List<ServiceLog>();
}
