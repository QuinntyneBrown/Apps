// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;

namespace ClassicCarRestorationLog.Api.Features.Projects;

public class ProjectDto
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public string CarMake { get; set; } = string.Empty;
    public string CarModel { get; set; } = string.Empty;
    public int? Year { get; set; }
    public ProjectPhase Phase { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public decimal? EstimatedBudget { get; set; }
    public decimal? ActualCost { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }

    public static ProjectDto FromEntity(Project project)
    {
        return new ProjectDto
        {
            ProjectId = project.ProjectId,
            UserId = project.UserId,
            CarMake = project.CarMake,
            CarModel = project.CarModel,
            Year = project.Year,
            Phase = project.Phase,
            StartDate = project.StartDate,
            CompletionDate = project.CompletionDate,
            EstimatedBudget = project.EstimatedBudget,
            ActualCost = project.ActualCost,
            Notes = project.Notes,
            CreatedAt = project.CreatedAt
        };
    }
}
