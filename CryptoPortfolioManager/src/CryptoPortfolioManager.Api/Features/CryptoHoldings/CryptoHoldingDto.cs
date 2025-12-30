// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CryptoPortfolioManager.Api.Features.CryptoHoldings;

public class CryptoHoldingDto
{
    public Guid CryptoHoldingId { get; set; }
    public Guid WalletId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal AverageCost { get; set; }
    public decimal CurrentPrice { get; set; }
    public DateTime LastPriceUpdate { get; set; }
    public decimal MarketValue { get; set; }
    public decimal UnrealizedGainLoss { get; set; }
}
