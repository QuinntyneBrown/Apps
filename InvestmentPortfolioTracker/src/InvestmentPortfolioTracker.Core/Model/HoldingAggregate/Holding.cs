// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core;

/// <summary>
/// Represents a security holding in a portfolio.
/// </summary>
public class Holding
{
    /// <summary>
    /// Gets or sets the unique identifier for the holding.
    /// </summary>
    public Guid HoldingId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the account.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Gets or sets the security symbol (ticker).
    /// </summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the security name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of shares held.
    /// </summary>
    public decimal Shares { get; set; }

    /// <summary>
    /// Gets or sets the average cost per share.
    /// </summary>
    public decimal AverageCost { get; set; }

    /// <summary>
    /// Gets or sets the current price per share.
    /// </summary>
    public decimal CurrentPrice { get; set; }

    /// <summary>
    /// Gets or sets the date when the price was last updated.
    /// </summary>
    public DateTime LastPriceUpdate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the account.
    /// </summary>
    public Account? Account { get; set; }

    /// <summary>
    /// Calculates the total market value of the holding.
    /// </summary>
    /// <returns>The market value.</returns>
    public decimal CalculateMarketValue()
    {
        return Shares * CurrentPrice;
    }

    /// <summary>
    /// Calculates the total cost basis of the holding.
    /// </summary>
    /// <returns>The cost basis.</returns>
    public decimal CalculateCostBasis()
    {
        return Shares * AverageCost;
    }

    /// <summary>
    /// Calculates the unrealized gain or loss.
    /// </summary>
    /// <returns>The gain (positive) or loss (negative).</returns>
    public decimal CalculateUnrealizedGainLoss()
    {
        return CalculateMarketValue() - CalculateCostBasis();
    }

    /// <summary>
    /// Updates the current price.
    /// </summary>
    /// <param name="newPrice">The new price per share.</param>
    public void UpdatePrice(decimal newPrice)
    {
        CurrentPrice = newPrice;
        LastPriceUpdate = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds shares to the holding.
    /// </summary>
    /// <param name="sharesToAdd">Number of shares to add.</param>
    /// <param name="pricePerShare">Price paid per share.</param>
    public void AddShares(decimal sharesToAdd, decimal pricePerShare)
    {
        decimal totalCost = (Shares * AverageCost) + (sharesToAdd * pricePerShare);
        Shares += sharesToAdd;
        AverageCost = Shares > 0 ? totalCost / Shares : 0;
    }

    /// <summary>
    /// Removes shares from the holding.
    /// </summary>
    /// <param name="sharesToRemove">Number of shares to remove.</param>
    public void RemoveShares(decimal sharesToRemove)
    {
        Shares = Math.Max(0, Shares - sharesToRemove);
    }
}
