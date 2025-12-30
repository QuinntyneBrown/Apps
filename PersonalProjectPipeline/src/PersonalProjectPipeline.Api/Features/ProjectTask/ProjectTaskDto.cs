// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Api.Features.ProjectTask;

/// <summary>
/// Data transfer object for ProjectTask.
/// </summary>
public record ProjectTaskDto
{
    public Guid ProjectTaskId { get; init; }
    public Guid ProjectId { get; init; }
    public Guid? MilestoneId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletionDate { get; init; }
    public double? EstimatedHours { get; init; }
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for ProjectTask entity.
/// </summary>
public static class ProjectTaskExtensions
{
    /// <summary>
    /// Converts a ProjectTask entity to a ProjectTaskDto.
    /// </summary>
    /// <param name="task">The task entity.</param>
    /// <returns>The task DTO.</returns>
    public static ProjectTaskDto ToDto(this Core.ProjectTask task)
    {
        return new ProjectTaskDto
        {
            ProjectTaskId = task.ProjectTaskId,
            ProjectId = task.ProjectId,
            MilestoneId = task.MilestoneId,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            IsCompleted = task.IsCompleted,
            CompletionDate = task.CompletionDate,
            EstimatedHours = task.EstimatedHours,
            CreatedAt = task.CreatedAt
        };
    }
}
