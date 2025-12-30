// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core;

/// <summary>
/// Represents a professional project.
/// </summary>
public class Project
{
    /// <summary>
    /// Gets or sets the unique identifier for the project.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this project.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the project.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed description of the project.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the organization or company where the project occurred.
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// Gets or sets the role in the project.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Gets or sets the project start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the project end date.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the collection of technologies used.
    /// </summary>
    public List<string> Technologies { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the collection of outcomes or results.
    /// </summary>
    public List<string> Outcomes { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the URL to the project (if applicable).
    /// </summary>
    public string? ProjectUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this project is featured on the resume.
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
    /// Toggles the featured status of this project.
    /// </summary>
    public void ToggleFeatured()
    {
        IsFeatured = !IsFeatured;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds a technology to this project.
    /// </summary>
    /// <param name="technology">The technology to add.</param>
    public void AddTechnology(string technology)
    {
        if (!Technologies.Contains(technology, StringComparer.OrdinalIgnoreCase))
        {
            Technologies.Add(technology);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Adds an outcome to this project.
    /// </summary>
    /// <param name="outcome">The outcome to add.</param>
    public void AddOutcome(string outcome)
    {
        if (!Outcomes.Contains(outcome))
        {
            Outcomes.Add(outcome);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Marks the project as completed.
    /// </summary>
    /// <param name="endDate">The completion date.</param>
    public void Complete(DateTime endDate)
    {
        EndDate = endDate;
        UpdatedAt = DateTime.UtcNow;
    }
}
