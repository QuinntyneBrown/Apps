// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Data transfer object for Project.
/// </summary>
public record ProjectDto
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the project name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the project description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the project status.
    /// </summary>
    public ProjectStatus Status { get; init; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime? StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime? EndDate { get; init; }

    /// <summary>
    /// Gets or sets the estimated cost.
    /// </summary>
    public decimal? EstimatedCost { get; init; }

    /// <summary>
    /// Gets or sets the actual cost.
    /// </summary>
    public decimal? ActualCost { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Project.
/// </summary>
public static class ProjectExtensions
{
    /// <summary>
    /// Converts a Project to a DTO.
    /// </summary>
    /// <param name="project">The project.</param>
    /// <returns>The DTO.</returns>
    public static ProjectDto ToDto(this Project project)
    {
        return new ProjectDto
        {
            ProjectId = project.ProjectId,
            UserId = project.UserId,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            EstimatedCost = project.EstimatedCost,
            ActualCost = project.ActualCost,
            CreatedAt = project.CreatedAt,
        };
    }
}
