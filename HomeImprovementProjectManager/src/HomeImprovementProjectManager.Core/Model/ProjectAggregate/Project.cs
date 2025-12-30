// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core;

/// <summary>
/// Represents a home improvement project.
/// </summary>
public class Project
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? EstimatedCost { get; set; }
    public decimal? ActualCost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    public ICollection<Contractor> Contractors { get; set; } = new List<Contractor>();
    public ICollection<Material> Materials { get; set; } = new List<Material>();
}
