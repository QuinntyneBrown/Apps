// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.ProgressRecords;

/// <summary>
/// Data transfer object for ProgressRecord.
/// </summary>
public class ProgressRecordDto
{
    public Guid ProgressRecordId { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkoutId { get; set; }
    public int ActualDurationMinutes { get; set; }
    public int? CaloriesBurned { get; set; }
    public int? PerformanceRating { get; set; }
    public string? Notes { get; set; }
    public string? ExerciseDetails { get; set; }
    public DateTime CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Extension methods for converting ProgressRecord entities to DTOs.
/// </summary>
public static class ProgressRecordExtensions
{
    /// <summary>
    /// Converts a ProgressRecord entity to a ProgressRecordDto.
    /// </summary>
    /// <param name="progressRecord">The progress record entity.</param>
    /// <returns>The progress record DTO.</returns>
    public static ProgressRecordDto ToDto(this ProgressRecord progressRecord)
    {
        return new ProgressRecordDto
        {
            ProgressRecordId = progressRecord.ProgressRecordId,
            UserId = progressRecord.UserId,
            WorkoutId = progressRecord.WorkoutId,
            ActualDurationMinutes = progressRecord.ActualDurationMinutes,
            CaloriesBurned = progressRecord.CaloriesBurned,
            PerformanceRating = progressRecord.PerformanceRating,
            Notes = progressRecord.Notes,
            ExerciseDetails = progressRecord.ExerciseDetails,
            CompletedAt = progressRecord.CompletedAt,
            CreatedAt = progressRecord.CreatedAt
        };
    }
}
