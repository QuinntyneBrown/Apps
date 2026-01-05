// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core;

/// <summary>
/// Represents a 529 college savings plan.
/// </summary>
public class Plan
{
    /// <summary>
    /// Gets or sets the unique identifier for the plan.
    /// </summary>
    public Guid PlanId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the plan name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the state sponsoring the plan.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the account number.
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Gets or sets the current balance.
    /// </summary>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// Gets or sets the date when the plan was opened.
    /// </summary>
    public DateTime OpenedDate { get; set; }

    /// <summary>
    /// Gets or sets the plan administrator.
    /// </summary>
    public string? Administrator { get; set; }

    /// <summary>
    /// Gets or sets whether the plan is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets notes about the plan.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Updates the plan balance.
    /// </summary>
    /// <param name="newBalance">The new balance.</param>
    public void UpdateBalance(decimal newBalance)
    {
        if (newBalance < 0)
        {
            throw new ArgumentException("Balance cannot be negative.", nameof(newBalance));
        }

        CurrentBalance = newBalance;
    }

    /// <summary>
    /// Closes the plan.
    /// </summary>
    public void Close()
    {
        IsActive = false;
    }
}
