// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Api.Features.TastingNotes;

/// <summary>
/// Data transfer object for TastingNote.
/// </summary>
public class TastingNoteDto
{
    /// <summary>
    /// Gets or sets the tasting note ID.
    /// </summary>
    public Guid TastingNoteId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the batch ID.
    /// </summary>
    public Guid BatchId { get; set; }

    /// <summary>
    /// Gets or sets the tasting date.
    /// </summary>
    public DateTime TastingDate { get; set; }

    /// <summary>
    /// Gets or sets the rating.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the appearance.
    /// </summary>
    public string? Appearance { get; set; }

    /// <summary>
    /// Gets or sets the aroma.
    /// </summary>
    public string? Aroma { get; set; }

    /// <summary>
    /// Gets or sets the flavor.
    /// </summary>
    public string? Flavor { get; set; }

    /// <summary>
    /// Gets or sets the mouthfeel.
    /// </summary>
    public string? Mouthfeel { get; set; }

    /// <summary>
    /// Gets or sets the overall impression.
    /// </summary>
    public string? OverallImpression { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
