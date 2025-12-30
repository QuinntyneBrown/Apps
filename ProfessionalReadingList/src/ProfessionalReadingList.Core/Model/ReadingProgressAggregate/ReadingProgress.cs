// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core;

/// <summary>
/// Represents reading progress for a resource.
/// </summary>
public class ReadingProgress
{
    /// <summary>
    /// Gets or sets the unique identifier for the reading progress.
    /// </summary>
    public Guid ReadingProgressId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this progress.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the resource ID.
    /// </summary>
    public Guid ResourceId { get; set; }

    /// <summary>
    /// Gets or sets the status (Not Started, Reading, Completed, Abandoned).
    /// </summary>
    public string Status { get; set; } = "Not Started";

    /// <summary>
    /// Gets or sets the current page or progress point.
    /// </summary>
    public int? CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the progress percentage (0-100).
    /// </summary>
    public int ProgressPercentage { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// Gets or sets the rating (1-5 stars).
    /// </summary>
    public int? Rating { get; set; }

    /// <summary>
    /// Gets or sets the review or thoughts.
    /// </summary>
    public string? Review { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the resource.
    /// </summary>
    public Resource? Resource { get; set; }

    /// <summary>
    /// Updates the progress.
    /// </summary>
    /// <param name="currentPage">The current page.</param>
    /// <param name="percentage">The progress percentage.</param>
    public void UpdateProgress(int? currentPage, int percentage)
    {
        CurrentPage = currentPage;
        ProgressPercentage = Math.Clamp(percentage, 0, 100);
        if (ProgressPercentage == 100 && Status != "Completed")
        {
            Complete();
        }
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Starts reading the resource.
    /// </summary>
    public void StartReading()
    {
        Status = "Reading";
        StartDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the resource as completed.
    /// </summary>
    public void Complete()
    {
        Status = "Completed";
        CompletionDate = DateTime.UtcNow;
        ProgressPercentage = 100;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds a rating and review.
    /// </summary>
    /// <param name="rating">The rating (1-5).</param>
    /// <param name="review">The review text.</param>
    public void AddRating(int rating, string? review)
    {
        Rating = Math.Clamp(rating, 1, 5);
        Review = review;
        UpdatedAt = DateTime.UtcNow;
    }
}
