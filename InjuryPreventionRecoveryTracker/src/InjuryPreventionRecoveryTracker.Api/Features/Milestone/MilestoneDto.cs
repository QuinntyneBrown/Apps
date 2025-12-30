// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Data transfer object for Milestone.
/// </summary>
public record MilestoneDto
{
    /// <summary>
    /// Gets or sets the milestone ID.
    /// </summary>
    public Guid MilestoneId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the target date.
    /// </summary>
    public DateTime? TargetDate { get; init; }

    /// <summary>
    /// Gets or sets the achieved date.
    /// </summary>
    public DateTime? AchievedDate { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the milestone is achieved.
    /// </summary>
    public bool IsAchieved { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Milestone.
/// </summary>
public static class MilestoneExtensions
{
    /// <summary>
    /// Converts a Milestone to a DTO.
    /// </summary>
    /// <param name="milestone">The milestone.</param>
    /// <returns>The DTO.</returns>
    public static MilestoneDto ToDto(this Milestone milestone)
    {
        return new MilestoneDto
        {
            MilestoneId = milestone.MilestoneId,
            UserId = milestone.UserId,
            InjuryId = milestone.InjuryId,
            Name = milestone.Name,
            Description = milestone.Description,
            TargetDate = milestone.TargetDate,
            AchievedDate = milestone.AchievedDate,
            IsAchieved = milestone.IsAchieved,
            Notes = milestone.Notes,
            CreatedAt = milestone.CreatedAt,
        };
    }
}
