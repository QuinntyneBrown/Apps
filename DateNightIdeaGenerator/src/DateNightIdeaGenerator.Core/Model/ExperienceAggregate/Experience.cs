// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DateNightIdeaGenerator.Core;

/// <summary>
/// Represents an experience of executing a date idea.
/// </summary>
public class Experience
{
    /// <summary>
    /// Gets or sets the unique identifier for the experience.
    /// </summary>
    public Guid ExperienceId { get; set; }

    /// <summary>
    /// Gets or sets the date idea ID this experience is associated with.
    /// </summary>
    public Guid DateIdeaId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who recorded this experience.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the date when the experience occurred.
    /// </summary>
    public DateTime ExperienceDate { get; set; }

    /// <summary>
    /// Gets or sets the notes or journal entry about the experience.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the actual cost of the date.
    /// </summary>
    public decimal? ActualCost { get; set; }

    /// <summary>
    /// Gets or sets the photos or media URLs from the experience.
    /// </summary>
    public string? Photos { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this was a successful date.
    /// </summary>
    public bool WasSuccessful { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the user would repeat this date.
    /// </summary>
    public bool WouldRepeat { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date idea this experience belongs to.
    /// </summary>
    public DateIdea? DateIdea { get; set; }

    /// <summary>
    /// Gets or sets the collection of ratings for this experience.
    /// </summary>
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    /// <summary>
    /// Marks the experience as successful.
    /// </summary>
    public void MarkAsSuccessful()
    {
        WasSuccessful = true;
    }

    /// <summary>
    /// Marks the experience as would repeat.
    /// </summary>
    public void MarkAsWouldRepeat()
    {
        WouldRepeat = true;
    }
}
