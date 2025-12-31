// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core;

public class Lease
{
    public Guid LeaseId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid PropertyId { get; set; }
    public string TenantName { get; set; } = string.Empty;
    public decimal MonthlyRent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal SecurityDeposit { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
    public Property? Property { get; set; }
    
    public void Terminate()
    {
        IsActive = false;
    }
}
