// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;

namespace InvestmentPortfolioTracker.Api.Features.Holding;

/// <summary>
/// Data transfer object for Holding.
/// </summary>
public record HoldingDto
{
    public Guid HoldingId { get; set; }
    public Guid AccountId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Shares { get; set; }
    public decimal AverageCost { get; set; }
    public decimal CurrentPrice { get; set; }
    public DateTime LastPriceUpdate { get; set; }
    public decimal MarketValue { get; set; }
    public decimal CostBasis { get; set; }
    public decimal UnrealizedGainLoss { get; set; }
}

/// <summary>
/// Extension methods for Holding entity.
/// </summary>
public static class HoldingExtensions
{
    /// <summary>
    /// Converts a Holding entity to HoldingDto.
    /// </summary>
    public static HoldingDto ToDto(this Core.Holding holding)
    {
        return new HoldingDto
        {
            HoldingId = holding.HoldingId,
            AccountId = holding.AccountId,
            Symbol = holding.Symbol,
            Name = holding.Name,
            Shares = holding.Shares,
            AverageCost = holding.AverageCost,
            CurrentPrice = holding.CurrentPrice,
            LastPriceUpdate = holding.LastPriceUpdate,
            MarketValue = holding.CalculateMarketValue(),
            CostBasis = holding.CalculateCostBasis(),
            UnrealizedGainLoss = holding.CalculateUnrealizedGainLoss()
        };
    }
}
