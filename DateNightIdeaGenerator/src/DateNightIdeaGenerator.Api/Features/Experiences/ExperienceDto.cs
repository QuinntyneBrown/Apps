// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DateNightIdeaGenerator.Api.Features.Experiences;

/// <summary>
/// Data transfer object for Experience.
/// </summary>
public class ExperienceDto
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
    public bool WasSuccessful { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user would repeat this date.
    /// </summary>
    public bool WouldRepeat { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
