// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core;

/// <summary>
/// Represents an investment account.
/// </summary>
public class Account
{
    /// <summary>
    /// Gets or sets the unique identifier for the account.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Gets or sets the account name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the account type.
    /// </summary>
    public AccountType AccountType { get; set; }

    /// <summary>
    /// Gets or sets the institution name.
    /// </summary>
    public string Institution { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the account number.
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Gets or sets the current balance.
    /// </summary>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// Gets or sets whether the account is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the date when the account was opened.
    /// </summary>
    public DateTime OpenedDate { get; set; }

    /// <summary>
    /// Gets or sets notes about the account.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Updates the account balance.
    /// </summary>
    /// <param name="newBalance">The new balance.</param>
    public void UpdateBalance(decimal newBalance)
    {
        CurrentBalance = newBalance;
    }

    /// <summary>
    /// Closes the account.
    /// </summary>
    public void Close()
    {
        IsActive = false;
    }
}
