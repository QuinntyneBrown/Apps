// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core;

/// <summary>
/// Represents an investment transaction.
/// </summary>
public class Transaction
{
    /// <summary>
    /// Gets or sets the unique identifier for the transaction.
    /// </summary>
    public Guid TransactionId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the account.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the holding (optional).
    /// </summary>
    public Guid? HoldingId { get; set; }

    /// <summary>
    /// Gets or sets the transaction date.
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Gets or sets the transaction type.
    /// </summary>
    public TransactionType TransactionType { get; set; }

    /// <summary>
    /// Gets or sets the security symbol (if applicable).
    /// </summary>
    public string? Symbol { get; set; }

    /// <summary>
    /// Gets or sets the number of shares (if applicable).
    /// </summary>
    public decimal? Shares { get; set; }

    /// <summary>
    /// Gets or sets the price per share (if applicable).
    /// </summary>
    public decimal? PricePerShare { get; set; }

    /// <summary>
    /// Gets or sets the transaction amount.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the fees associated with the transaction.
    /// </summary>
    public decimal? Fees { get; set; }

    /// <summary>
    /// Gets or sets notes about the transaction.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the account.
    /// </summary>
    public Account? Account { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the holding.
    /// </summary>
    public Holding? Holding { get; set; }

    /// <summary>
    /// Calculates the total cost including fees.
    /// </summary>
    /// <returns>The total cost.</returns>
    public decimal CalculateTotalCost()
    {
        return Amount + (Fees ?? 0);
    }
}
