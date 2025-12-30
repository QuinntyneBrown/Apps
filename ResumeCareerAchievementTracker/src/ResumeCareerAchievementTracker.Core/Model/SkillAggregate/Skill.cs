// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core;

/// <summary>
/// Represents a professional skill.
/// </summary>
public class Skill
{
    /// <summary>
    /// Gets or sets the unique identifier for the skill.
    /// </summary>
    public Guid SkillId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this skill.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the skill.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the skill (e.g., Programming, Leadership, Design).
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the proficiency level (1-5 or Beginner/Intermediate/Advanced/Expert).
    /// </summary>
    public string ProficiencyLevel { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the years of experience with this skill.
    /// </summary>
    public decimal? YearsOfExperience { get; set; }

    /// <summary>
    /// Gets or sets when the skill was last used.
    /// </summary>
    public DateTime? LastUsedDate { get; set; }

    /// <summary>
    /// Gets or sets optional notes about this skill.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this skill is featured on the resume.
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Updates the proficiency level of this skill.
    /// </summary>
    /// <param name="level">The new proficiency level.</param>
    public void UpdateProficiency(string level)
    {
        ProficiencyLevel = level;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Toggles the featured status of this skill.
    /// </summary>
    public void ToggleFeatured()
    {
        IsFeatured = !IsFeatured;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the last used date of this skill.
    /// </summary>
    /// <param name="date">The date the skill was last used.</param>
    public void UpdateLastUsed(DateTime date)
    {
        LastUsedDate = date;
        UpdatedAt = DateTime.UtcNow;
    }
}
