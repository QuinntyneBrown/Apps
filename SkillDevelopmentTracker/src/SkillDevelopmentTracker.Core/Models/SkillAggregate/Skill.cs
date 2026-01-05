// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Represents a skill being developed.
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
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the skill.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the skill (e.g., Programming, Design, Business).
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current proficiency level.
    /// </summary>
    public ProficiencyLevel ProficiencyLevel { get; set; }

    /// <summary>
    /// Gets or sets the target proficiency level.
    /// </summary>
    public ProficiencyLevel? TargetLevel { get; set; }

    /// <summary>
    /// Gets or sets the date when learning started.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets the total hours spent learning.
    /// </summary>
    public decimal HoursSpent { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the collection of associated course IDs.
    /// </summary>
    public List<Guid> CourseIds { get; set; } = new List<Guid>();

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Updates the proficiency level.
    /// </summary>
    /// <param name="newLevel">The new proficiency level.</param>
    public void UpdateProficiency(ProficiencyLevel newLevel)
    {
        ProficiencyLevel = newLevel;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds learning hours to the skill.
    /// </summary>
    /// <param name="hours">The number of hours to add.</param>
    public void AddHours(decimal hours)
    {
        HoursSpent += hours;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Associates a course with this skill.
    /// </summary>
    /// <param name="courseId">The course ID to associate.</param>
    public void AddCourse(Guid courseId)
    {
        if (!CourseIds.Contains(courseId))
        {
            CourseIds.Add(courseId);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
