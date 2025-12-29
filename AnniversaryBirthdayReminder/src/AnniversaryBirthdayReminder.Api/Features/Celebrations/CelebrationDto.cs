// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Data transfer object for Celebration.
/// </summary>
public record CelebrationDto
{
    /// <summary>
    /// Gets or sets the celebration ID.
    /// </summary>
    public Guid CelebrationId { get; init; }

    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets or sets the celebration date.
    /// </summary>
    public DateTime CelebrationDate { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets the photos.
    /// </summary>
    public List<string> Photos { get; init; } = new List<string>();

    /// <summary>
    /// Gets or sets the attendees.
    /// </summary>
    public List<string> Attendees { get; init; } = new List<string>();

    /// <summary>
    /// Gets or sets the rating.
    /// </summary>
    public int? Rating { get; init; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public CelebrationStatus Status { get; init; }
}

/// <summary>
/// Extension methods for Celebration.
/// </summary>
public static class CelebrationExtensions
{
    /// <summary>
    /// Converts a Celebration to a DTO.
    /// </summary>
    /// <param name="celebration">The celebration.</param>
    /// <returns>The DTO.</returns>
    public static CelebrationDto ToDto(this Celebration celebration)
    {
        return new CelebrationDto
        {
            CelebrationId = celebration.CelebrationId,
            ImportantDateId = celebration.ImportantDateId,
            CelebrationDate = celebration.CelebrationDate,
            Notes = celebration.Notes,
            Photos = celebration.Photos,
            Attendees = celebration.Attendees,
            Rating = celebration.Rating,
            Status = celebration.Status,
        };
    }
}
