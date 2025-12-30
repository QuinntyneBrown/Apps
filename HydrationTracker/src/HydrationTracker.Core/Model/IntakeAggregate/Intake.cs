// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core;

public class Intake
{
    public Guid IntakeId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public BeverageType BeverageType { get; set; }
    public decimal AmountMl { get; set; }
    public DateTime IntakeTime { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public decimal GetAmountInOz()
    {
        return AmountMl * 0.033814m;
    }
}
