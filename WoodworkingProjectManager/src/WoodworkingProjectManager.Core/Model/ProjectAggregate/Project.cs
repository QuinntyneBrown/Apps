// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WoodworkingProjectManager.Core;

/// <summary>
/// Represents a woodworking project.
/// </summary>
public class Project
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public WoodType WoodType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public decimal? EstimatedCost { get; set; }
    public decimal? ActualCost { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Material> Materials { get; set; } = new List<Material>();

    public void MarkAsCompleted()
    {
        Status = ProjectStatus.Completed;
        CompletionDate = DateTime.UtcNow;
    }
}
