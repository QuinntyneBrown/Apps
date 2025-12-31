// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RetirementSavingsCalculator.Core;

/// <summary>
/// Represents a retirement contribution.
/// </summary>
public class Contribution
{
    /// <summary>
    /// Gets or sets the unique identifier for the contribution.
    /// </summary>
    public Guid ContributionId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the retirement scenario.
    /// </summary>
    public Guid RetirementScenarioId { get; set; }

    /// <summary>
    /// Gets or sets the contribution amount.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the contribution date.
    /// </summary>
    public DateTime ContributionDate { get; set; }

    /// <summary>
    /// Gets or sets the account name.
    /// </summary>
    public string AccountName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether this is an employer match.
    /// </summary>
    public bool IsEmployerMatch { get; set; }

    /// <summary>
    /// Gets or sets notes about the contribution.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the retirement scenario.
    /// </summary>
    public RetirementScenario? RetirementScenario { get; set; }

    /// <summary>
    /// Validates the contribution amount.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when amount is not positive.</exception>
    public void ValidateAmount()
    {
        if (Amount <= 0)
        {
            throw new ArgumentException("Contribution amount must be positive.", nameof(Amount));
        }
    }
}
