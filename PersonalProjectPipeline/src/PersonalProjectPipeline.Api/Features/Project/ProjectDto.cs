// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalProjectPipeline.Core;

namespace PersonalProjectPipeline.Api.Features.Project;

/// <summary>
/// Data transfer object for Project.
/// </summary>
public record ProjectDto
{
    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public ProjectStatus Status { get; init; }
    public ProjectPriority Priority { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? TargetDate { get; init; }
    public DateTime? CompletionDate { get; init; }
    public string? Tags { get; init; }
    public DateTime CreatedAt { get; init; }
    public double ProgressPercentage { get; init; }
}

/// <summary>
/// Extension methods for Project entity.
/// </summary>
public static class ProjectExtensions
{
    /// <summary>
    /// Converts a Project entity to a ProjectDto.
    /// </summary>
    /// <param name="project">The project entity.</param>
    /// <returns>The project DTO.</returns>
    public static ProjectDto ToDto(this Core.Project project)
    {
        return new ProjectDto
        {
            ProjectId = project.ProjectId,
            UserId = project.UserId,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            Priority = project.Priority,
            StartDate = project.StartDate,
            TargetDate = project.TargetDate,
            CompletionDate = project.CompletionDate,
            Tags = project.Tags,
            CreatedAt = project.CreatedAt,
            ProgressPercentage = project.GetProgressPercentage()
        };
    }
}
