// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core;

/// <summary>
/// Represents a career achievement or accomplishment.
/// </summary>
public class Achievement
{
    /// <summary>
    /// Gets or sets the unique identifier for the achievement.
    /// </summary>
    public Guid AchievementId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this achievement.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the achievement.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed description of the achievement.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of achievement.
    /// </summary>
    public AchievementType AchievementType { get; set; }

    /// <summary>
    /// Gets or sets the date when the achievement was accomplished.
    /// </summary>
    public DateTime AchievedDate { get; set; }

    /// <summary>
    /// Gets or sets the organization or company where the achievement occurred.
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// Gets or sets the quantifiable impact or result.
    /// </summary>
    public string? Impact { get; set; }

    /// <summary>
    /// Gets or sets the collection of associated skill IDs.
    /// </summary>
    public List<Guid> SkillIds { get; set; } = new List<Guid>();

    /// <summary>
    /// Gets or sets the collection of associated project IDs.
    /// </summary>
    public List<Guid> ProjectIds { get; set; } = new List<Guid>();

    /// <summary>
    /// Gets or sets a value indicating whether this achievement is featured on the resume.
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Gets or sets optional tags for categorization.
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Toggles the featured status of this achievement.
    /// </summary>
    public void ToggleFeatured()
    {
        IsFeatured = !IsFeatured;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Associates a skill with this achievement.
    /// </summary>
    /// <param name="skillId">The skill ID to associate.</param>
    public void AddSkill(Guid skillId)
    {
        if (!SkillIds.Contains(skillId))
        {
            SkillIds.Add(skillId);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Associates a project with this achievement.
    /// </summary>
    /// <param name="projectId">The project ID to associate.</param>
    public void AddProject(Guid projectId)
    {
        if (!ProjectIds.Contains(projectId))
        {
            ProjectIds.Add(projectId);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Adds a tag to this achievement.
    /// </summary>
    /// <param name="tag">The tag to add.</param>
    public void AddTag(string tag)
    {
        if (!Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
        {
            Tags.Add(tag);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
