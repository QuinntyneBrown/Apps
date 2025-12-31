// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core;

public class Contribution
{
    public Guid ContributionId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid GoalId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ContributionDate { get; set; }
    public string? Notes { get; set; }
    public Goal? Goal { get; set; }
}
