// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Api.Features.CompletionLogs;

/// <summary>
/// Data transfer object for CompletionLog.
/// </summary>
public record CompletionLogDto
{
    public Guid CompletionLogId { get; set; }
    public Guid RoutineId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CompletionDate { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualEndTime { get; set; }
    public int TasksCompleted { get; set; }
    public int TotalTasks { get; set; }
    public string? Notes { get; set; }
    public int? MoodRating { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Request to create a new completion log.
/// </summary>
public record CreateCompletionLogRequest
{
    public Guid RoutineId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CompletionDate { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualEndTime { get; set; }
    public int TasksCompleted { get; set; }
    public int TotalTasks { get; set; }
    public string? Notes { get; set; }
    public int? MoodRating { get; set; }
}

/// <summary>
/// Request to update an existing completion log.
/// </summary>
public record UpdateCompletionLogRequest
{
    public DateTime CompletionDate { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualEndTime { get; set; }
    public int TasksCompleted { get; set; }
    public int TotalTasks { get; set; }
    public string? Notes { get; set; }
    public int? MoodRating { get; set; }
}
