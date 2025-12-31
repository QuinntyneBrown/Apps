// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core;

public class Goal
{
    public Guid GoalId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public GoalType GoalType { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public DateTime TargetDate { get; set; }
    public GoalStatus Status { get; set; }
    public string? Notes { get; set; }
    public List<Milestone> Milestones { get; set; } = new List<Milestone>();
    
    public decimal CalculateProgress()
    {
        if (TargetAmount == 0) return 0;
        return (CurrentAmount / TargetAmount) * 100;
    }
    
    public void UpdateProgress(decimal amount)
    {
        CurrentAmount += amount;
        if (CurrentAmount >= TargetAmount)
        {
            Status = GoalStatus.Completed;
        }
    }
}
