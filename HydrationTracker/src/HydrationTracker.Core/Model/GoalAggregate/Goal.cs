// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core;

public class Goal
{
    public Guid GoalId { get; set; }
    public Guid UserId { get; set; }
    public decimal DailyGoalMl { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public decimal GetDailyGoalInOz()
    {
        return DailyGoalMl * 0.033814m;
    }
}
