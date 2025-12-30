// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Represents a structured learning path or curriculum.
/// </summary>
public class LearningPath
{
    /// <summary>
    /// Gets or sets the unique identifier for the learning path.
    /// </summary>
    public Guid LearningPathId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this learning path.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the title of the learning path.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description or goal of the learning path.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets the actual completion date.
    /// </summary>
    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// Gets or sets the collection of course IDs in this path.
    /// </summary>
    public List<Guid> CourseIds { get; set; } = new List<Guid>();

    /// <summary>
    /// Gets or sets the collection of skill IDs being developed.
    /// </summary>
    public List<Guid> SkillIds { get; set; } = new List<Guid>();

    /// <summary>
    /// Gets or sets the overall progress percentage (0-100).
    /// </summary>
    public int ProgressPercentage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the path is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Updates the progress of the learning path.
    /// </summary>
    /// <param name="percentage">The new progress percentage.</param>
    public void UpdateProgress(int percentage)
    {
        ProgressPercentage = Math.Clamp(percentage, 0, 100);
        if (ProgressPercentage == 100 && !IsCompleted)
        {
            Complete();
        }
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the learning path as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletionDate = DateTime.UtcNow;
        ProgressPercentage = 100;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds a course to this learning path.
    /// </summary>
    /// <param name="courseId">The course ID to add.</param>
    public void AddCourse(Guid courseId)
    {
        if (!CourseIds.Contains(courseId))
        {
            CourseIds.Add(courseId);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Adds a skill to this learning path.
    /// </summary>
    /// <param name="skillId">The skill ID to add.</param>
    public void AddSkill(Guid skillId)
    {
        if (!SkillIds.Contains(skillId))
        {
            SkillIds.Add(skillId);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
