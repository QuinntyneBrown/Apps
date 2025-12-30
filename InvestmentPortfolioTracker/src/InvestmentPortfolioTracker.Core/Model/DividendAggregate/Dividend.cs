// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core;

/// <summary>
/// Represents a dividend payment.
/// </summary>
public class Dividend
{
    /// <summary>
    /// Gets or sets the unique identifier for the dividend.
    /// </summary>
    public Guid DividendId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the holding.
    /// </summary>
    public Guid HoldingId { get; set; }

    /// <summary>
    /// Gets or sets the payment date.
    /// </summary>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Gets or sets the ex-dividend date.
    /// </summary>
    public DateTime ExDividendDate { get; set; }

    /// <summary>
    /// Gets or sets the dividend amount per share.
    /// </summary>
    public decimal AmountPerShare { get; set; }

    /// <summary>
    /// Gets or sets the total dividend amount.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets whether the dividend was reinvested.
    /// </summary>
    public bool IsReinvested { get; set; }

    /// <summary>
    /// Gets or sets notes about the dividend.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the holding.
    /// </summary>
    public Holding? Holding { get; set; }

    /// <summary>
    /// Calculates the annual dividend yield based on current price.
    /// </summary>
    /// <param name="currentPrice">The current share price.</param>
    /// <param name="paymentsPerYear">Number of dividend payments per year.</param>
    /// <returns>The annualized yield percentage.</returns>
    public decimal CalculateYield(decimal currentPrice, int paymentsPerYear = 4)
    {
        if (currentPrice <= 0)
        {
            return 0;
        }

        return (AmountPerShare * paymentsPerYear / currentPrice) * 100;
    }
}
