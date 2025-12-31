// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core;

public class ServiceRecord
{
    public Guid ServiceRecordId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid ApplianceId { get; set; }
    public Appliance? Appliance { get; set; }
    public DateTime ServiceDate { get; set; }
    public string? ServiceProvider { get; set; }
    public string? Description { get; set; }
    public decimal? Cost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
