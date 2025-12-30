// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Api.Features.Priorities;

/// <summary>
/// Data transfer object for WeeklyPriority.
/// </summary>
public class WeeklyPriorityDto
{
    public Guid WeeklyPriorityId { get; set; }
    public Guid WeeklyReviewId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PriorityLevel Level { get; set; }
    public string? Category { get; set; }
    public DateTime? TargetDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Extension methods for WeeklyPriority entity.
/// </summary>
public static class WeeklyPriorityExtensions
{
    /// <summary>
    /// Converts a WeeklyPriority entity to a DTO.
    /// </summary>
    public static WeeklyPriorityDto ToDto(this WeeklyPriority priority)
    {
        return new WeeklyPriorityDto
        {
            WeeklyPriorityId = priority.WeeklyPriorityId,
            WeeklyReviewId = priority.WeeklyReviewId,
            Title = priority.Title,
            Description = priority.Description,
            Level = priority.Level,
            Category = priority.Category,
            TargetDate = priority.TargetDate,
            IsCompleted = priority.IsCompleted,
            CreatedAt = priority.CreatedAt
        };
    }
}
