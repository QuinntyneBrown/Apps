// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core;

public class Budget
{
    public Guid BudgetId { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal AllocatedAmount { get; set; }
    public decimal? SpentAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
