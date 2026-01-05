// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents a celebration record for an important date.
/// </summary>
public class Celebration
{
    /// <summary>
    /// Gets or sets the unique identifier for the celebration.
    /// </summary>
    public Guid CelebrationId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the important date.
    /// </summary>
    public Guid ImportantDateId { get; set; }

    /// <summary>
    /// Gets or sets the date when the celebration occurred.
    /// </summary>
    public DateTime CelebrationDate { get; set; }

    /// <summary>
    /// Gets or sets notes about the celebration.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the list of photo URLs.
    /// </summary>
    public List<string> Photos { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the list of attendees.
    /// </summary>
    public List<string> Attendees { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the rating of the celebration (1-5).
    /// </summary>
    public int? Rating { get; set; }

    /// <summary>
    /// Gets or sets the status of the celebration.
    /// </summary>
    public CelebrationStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the important date.
    /// </summary>
    public ImportantDate? ImportantDate { get; set; }

    /// <summary>
    /// Marks the celebration as completed.
    /// </summary>
    /// <param name="notes">Optional notes about the celebration.</param>
    /// <param name="rating">Optional rating (1-5).</param>
    public void MarkAsCompleted(string? notes = null, int? rating = null)
    {
        Status = CelebrationStatus.Completed;
        Notes = notes;
        if (rating.HasValue)
        {
            SetRating(rating.Value);
        }
    }

    /// <summary>
    /// Marks the celebration as skipped.
    /// </summary>
    /// <param name="reason">Optional reason for skipping.</param>
    public void MarkAsSkipped(string? reason = null)
    {
        Status = CelebrationStatus.Skipped;
        Notes = reason;
    }

    /// <summary>
    /// Sets the rating for the celebration.
    /// </summary>
    /// <param name="rating">The rating value (1-5).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when rating is not between 1 and 5.</exception>
    public void SetRating(int rating)
    {
        if (rating < 1 || rating > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");
        }

        Rating = rating;
    }

    /// <summary>
    /// Adds photos to the celebration.
    /// </summary>
    /// <param name="photoUrls">The photo URLs to add.</param>
    public void AddPhotos(IEnumerable<string> photoUrls)
    {
        Photos.AddRange(photoUrls);
    }

    /// <summary>
    /// Adds attendees to the celebration.
    /// </summary>
    /// <param name="attendeeNames">The attendee names to add.</param>
    public void AddAttendees(IEnumerable<string> attendeeNames)
    {
        Attendees.AddRange(attendeeNames);
    }
}
