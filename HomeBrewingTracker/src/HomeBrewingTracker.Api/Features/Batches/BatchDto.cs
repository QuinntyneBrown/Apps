// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;

namespace HomeBrewingTracker.Api.Features.Batches;

/// <summary>
/// Data transfer object for Batch.
/// </summary>
public class BatchDto
{
    /// <summary>
    /// Gets or sets the batch ID.
    /// </summary>
    public Guid BatchId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the batch number.
    /// </summary>
    public string BatchNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public BatchStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the brew date.
    /// </summary>
    public DateTime BrewDate { get; set; }

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
    /// Gets or sets the actual ABV.
    /// </summary>
    public decimal? ActualABV { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
