// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeEnergyUsageTracker.Core;

public class UtilityBill
{
    public Guid UtilityBillId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public UtilityType UtilityType { get; set; }
    public DateTime BillingDate { get; set; }
    public decimal Amount { get; set; }
    public decimal? UsageAmount { get; set; }
    public string? Unit { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
