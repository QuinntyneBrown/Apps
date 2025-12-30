// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core;

/// <summary>
/// Represents a brewing batch.
/// </summary>
public class Batch
{
    /// <summary>
    /// Gets or sets the unique identifier for the batch.
    /// </summary>
    public Guid BatchId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this batch.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the recipe ID used for this batch.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the recipe used for this batch.
    /// </summary>
    public Recipe? Recipe { get; set; }

    /// <summary>
    /// Gets or sets the batch number.
    /// </summary>
    public string BatchNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the batch status.
    /// </summary>
    public BatchStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the brew date.
    /// </summary>
    public DateTime BrewDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the bottling date.
    /// </summary>
    public DateTime? BottlingDate { get; set; }

    /// <summary>
    /// Gets or sets the actual original gravity.
    /// </summary>
    public decimal? ActualOriginalGravity { get; set; }

    /// <summary>
    /// Gets or sets the actual final gravity.
    /// </summary>
    public decimal? ActualFinalGravity { get; set; }

    /// <summary>
    /// Gets or sets the actual ABV percentage.
    /// </summary>
    public decimal? ActualABV { get; set; }

    /// <summary>
    /// Gets or sets notes about this batch.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of tasting notes for this batch.
    /// </summary>
    public ICollection<TastingNote> TastingNotes { get; set; } = new List<TastingNote>();

    /// <summary>
    /// Marks the batch as bottled.
    /// </summary>
    public void MarkAsBottled()
    {
        Status = BatchStatus.Bottled;
        BottlingDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the batch as completed.
    /// </summary>
    public void MarkAsCompleted()
    {
        Status = BatchStatus.Completed;
    }
}
