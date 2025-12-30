// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core;

public class Project
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string CarMake { get; set; } = string.Empty;
    public string CarModel { get; set; } = string.Empty;
    public int? Year { get; set; }
    public ProjectPhase Phase { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? CompletionDate { get; set; }
    public decimal? EstimatedBudget { get; set; }
    public decimal? ActualCost { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Part> Parts { get; set; } = new List<Part>();
    public ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
    public ICollection<PhotoLog> PhotoLogs { get; set; } = new List<PhotoLog>();
}
