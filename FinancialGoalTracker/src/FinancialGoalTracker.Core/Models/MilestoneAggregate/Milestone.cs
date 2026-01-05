// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core;

public class Milestone
{
    public Guid MilestoneId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid GoalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal TargetAmount { get; set; }
    public DateTime TargetDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string? Notes { get; set; }
    public Goal? Goal { get; set; }
    
    public void MarkAsCompleted()
    {
        IsCompleted = true;
        CompletedDate = DateTime.UtcNow;
    }
}
