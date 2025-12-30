// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MorningRoutineBuilder.Core;

namespace MorningRoutineBuilder.Api.Features.RoutineTasks;

/// <summary>
/// Data transfer object for RoutineTask.
/// </summary>
public record RoutineTaskDto
{
    public Guid RoutineTaskId { get; set; }
    public Guid RoutineId { get; set; }
    public string Name { get; set; } = string.Empty;
    public TaskType TaskType { get; set; }
    public string? Description { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public int SortOrder { get; set; }
    public bool IsOptional { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Request to create a new routine task.
/// </summary>
public record CreateRoutineTaskRequest
{
    public Guid RoutineId { get; set; }
    public string Name { get; set; } = string.Empty;
    public TaskType TaskType { get; set; }
    public string? Description { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public int SortOrder { get; set; }
    public bool IsOptional { get; set; }
}

/// <summary>
/// Request to update an existing routine task.
/// </summary>
public record UpdateRoutineTaskRequest
{
    public string Name { get; set; } = string.Empty;
    public TaskType TaskType { get; set; }
    public string? Description { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public int SortOrder { get; set; }
    public bool IsOptional { get; set; }
}
