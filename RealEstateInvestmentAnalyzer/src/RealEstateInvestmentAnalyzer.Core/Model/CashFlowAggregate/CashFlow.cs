// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core;

public class CashFlow
{
    public Guid CashFlowId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid PropertyId { get; set; }
    public DateTime Date { get; set; }
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public decimal NetCashFlow { get; set; }
    public string? Notes { get; set; }
    public Property? Property { get; set; }
    
    public void CalculateNetCashFlow()
    {
        NetCashFlow = Income - Expenses;
    }
}
