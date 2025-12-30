// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CryptoPortfolioManager.Core;

public class Wallet
{
    public Guid WalletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string WalletType { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
    public List<CryptoHolding> Holdings { get; set; } = new List<CryptoHolding>();
    
    public decimal CalculateTotalValue()
    {
        return Holdings.Sum(h => h.CalculateMarketValue());
    }
}
