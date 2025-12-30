// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Api.Features.Milestone;

/// <summary>
/// Data transfer object for Milestone.
/// </summary>
public record MilestoneDto
{
    public Guid MilestoneId { get; init; }
    public Guid ProjectId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime? TargetDate { get; init; }
    public bool IsAchieved { get; init; }
    public DateTime? AchievementDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsOverdue { get; init; }
}

/// <summary>
/// Extension methods for Milestone entity.
/// </summary>
public static class MilestoneExtensions
{
    /// <summary>
    /// Converts a Milestone entity to a MilestoneDto.
    /// </summary>
    /// <param name="milestone">The milestone entity.</param>
    /// <returns>The milestone DTO.</returns>
    public static MilestoneDto ToDto(this Core.Milestone milestone)
    {
        return new MilestoneDto
        {
            MilestoneId = milestone.MilestoneId,
            ProjectId = milestone.ProjectId,
            Name = milestone.Name,
            Description = milestone.Description,
            TargetDate = milestone.TargetDate,
            IsAchieved = milestone.IsAchieved,
            AchievementDate = milestone.AchievementDate,
            CreatedAt = milestone.CreatedAt,
            IsOverdue = milestone.IsOverdue()
        };
    }
}
