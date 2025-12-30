// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core;

/// <summary>
/// Represents a tasting note for a batch.
/// </summary>
public class TastingNote
{
    /// <summary>
    /// Gets or sets the unique identifier for the tasting note.
    /// </summary>
    public Guid TastingNoteId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this tasting note.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the batch ID associated with this tasting note.
    /// </summary>
    public Guid BatchId { get; set; }

    /// <summary>
    /// Gets or sets the batch associated with this tasting note.
    /// </summary>
    public Batch? Batch { get; set; }

    /// <summary>
    /// Gets or sets the tasting date.
    /// </summary>
    public DateTime TastingDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the rating (1-5 stars).
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the appearance notes.
    /// </summary>
    public string? Appearance { get; set; }

    /// <summary>
    /// Gets or sets the aroma notes.
    /// </summary>
    public string? Aroma { get; set; }

    /// <summary>
    /// Gets or sets the flavor notes.
    /// </summary>
    public string? Flavor { get; set; }

    /// <summary>
    /// Gets or sets the mouthfeel notes.
    /// </summary>
    public string? Mouthfeel { get; set; }

    /// <summary>
    /// Gets or sets the overall impression.
    /// </summary>
    public string? OverallImpression { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Validates the rating is within valid range.
    /// </summary>
    /// <returns>True if rating is valid; otherwise, false.</returns>
    public bool IsRatingValid()
    {
        return Rating >= 1 && Rating <= 5;
    }
}
