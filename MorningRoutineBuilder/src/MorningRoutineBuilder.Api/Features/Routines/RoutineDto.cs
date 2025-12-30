// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Api.Features.Routines;

/// <summary>
/// Data transfer object for Routine.
/// </summary>
public record RoutineDto
{
    public Guid RoutineId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TimeSpan TargetStartTime { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Request to create a new routine.
/// </summary>
public record CreateRoutineRequest
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TimeSpan TargetStartTime { get; set; }
    public int EstimatedDurationMinutes { get; set; }
}

/// <summary>
/// Request to update an existing routine.
/// </summary>
public record UpdateRoutineRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TimeSpan TargetStartTime { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public bool IsActive { get; set; }
}
