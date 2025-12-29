// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Data transfer object for ImportantDate.
/// </summary>
public record ImportantDateDto
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the person name.
    /// </summary>
    public string PersonName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the date type.
    /// </summary>
    public DateType DateType { get; init; }

    /// <summary>
    /// Gets or sets the date value.
    /// </summary>
    public DateTime DateValue { get; init; }

    /// <summary>
    /// Gets or sets the recurrence pattern.
    /// </summary>
    public RecurrencePattern RecurrencePattern { get; init; }

    /// <summary>
    /// Gets or sets the relationship.
    /// </summary>
    public string? Relationship { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the date is active.
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets or sets the next occurrence date.
    /// </summary>
    public DateTime NextOccurrence { get; init; }
}

/// <summary>
/// Extension methods for ImportantDate.
/// </summary>
public static class ImportantDateExtensions
{
    /// <summary>
    /// Converts an ImportantDate to a DTO.
    /// </summary>
    /// <param name="importantDate">The important date.</param>
    /// <returns>The DTO.</returns>
    public static ImportantDateDto ToDto(this ImportantDate importantDate)
    {
        return new ImportantDateDto
        {
            ImportantDateId = importantDate.ImportantDateId,
            UserId = importantDate.UserId,
            PersonName = importantDate.PersonName,
            DateType = importantDate.DateType,
            DateValue = importantDate.DateValue,
            RecurrencePattern = importantDate.RecurrencePattern,
            Relationship = importantDate.Relationship,
            Notes = importantDate.Notes,
            IsActive = importantDate.IsActive,
            CreatedAt = importantDate.CreatedAt,
            NextOccurrence = importantDate.GetNextOccurrence(),
        };
    }
}
