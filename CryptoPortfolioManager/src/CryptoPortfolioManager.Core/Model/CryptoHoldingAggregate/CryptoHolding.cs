// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CryptoPortfolioManager.Core;

public class CryptoHolding
{
    public Guid CryptoHoldingId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid WalletId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal AverageCost { get; set; }
    public decimal CurrentPrice { get; set; }
    public DateTime LastPriceUpdate { get; set; } = DateTime.UtcNow;
    public Wallet? Wallet { get; set; }
    
    public decimal CalculateMarketValue()
    {
        return Quantity * CurrentPrice;
    }
    
    public decimal CalculateUnrealizedGainLoss()
    {
        return (CurrentPrice - AverageCost) * Quantity;
    }
    
    public void UpdatePrice(decimal newPrice)
    {
        CurrentPrice = newPrice;
        LastPriceUpdate = DateTime.UtcNow;
    }
}
