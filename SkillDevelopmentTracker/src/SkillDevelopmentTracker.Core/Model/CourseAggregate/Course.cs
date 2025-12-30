// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Represents a learning course or training.
/// </summary>
public class Course
{
    /// <summary>
    /// Gets or sets the unique identifier for the course.
    /// </summary>
    public Guid CourseId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this course.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the title of the course.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the course provider (e.g., Coursera, Udemy, LinkedIn Learning).
    /// </summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the instructor name.
    /// </summary>
    public string? Instructor { get; set; }

    /// <summary>
    /// Gets or sets the course URL.
    /// </summary>
    public string? CourseUrl { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// Gets or sets the progress percentage (0-100).
    /// </summary>
    public int ProgressPercentage { get; set; }

    /// <summary>
    /// Gets or sets the estimated duration in hours.
    /// </summary>
    public decimal? EstimatedHours { get; set; }

    /// <summary>
    /// Gets or sets the actual hours spent.
    /// </summary>
    public decimal ActualHours { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the course is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the collection of skill IDs this course develops.
    /// </summary>
    public List<Guid> SkillIds { get; set; } = new List<Guid>();

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
    /// Updates the progress of the course.
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
    /// Marks the course as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletionDate = DateTime.UtcNow;
        ProgressPercentage = 100;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds learning hours to the course.
    /// </summary>
    /// <param name="hours">The number of hours to add.</param>
    public void AddHours(decimal hours)
    {
        ActualHours += hours;
        UpdatedAt = DateTime.UtcNow;
    }
}
