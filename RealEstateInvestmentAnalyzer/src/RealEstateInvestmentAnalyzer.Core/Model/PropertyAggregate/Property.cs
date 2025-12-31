// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core;

public class Property
{
    public Guid PropertyId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Address { get; set; } = string.Empty;
    public PropertyType PropertyType { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal CurrentValue { get; set; }
    public int SquareFeet { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public string? Notes { get; set; }
    
    public decimal CalculateEquity()
    {
        return CurrentValue - PurchasePrice;
    }
    
    public decimal CalculateROI()
    {
        if (PurchasePrice == 0) return 0;
        return ((CurrentValue - PurchasePrice) / PurchasePrice) * 100;
    }
}
