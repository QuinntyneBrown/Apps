// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core;

/// <summary>
/// Represents the type of investment transaction.
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// Buy transaction (purchase of securities).
    /// </summary>
    Buy = 0,

    /// <summary>
    /// Sell transaction (sale of securities).
    /// </summary>
    Sell = 1,

    /// <summary>
    /// Dividend received.
    /// </summary>
    Dividend = 2,

    /// <summary>
    /// Interest received.
    /// </summary>
    Interest = 3,

    /// <summary>
    /// Deposit (cash contribution).
    /// </summary>
    Deposit = 4,

    /// <summary>
    /// Withdrawal (cash distribution).
    /// </summary>
    Withdrawal = 5,

    /// <summary>
    /// Transfer between accounts.
    /// </summary>
    Transfer = 6,

    /// <summary>
    /// Fee or expense.
    /// </summary>
    Fee = 7,
}
