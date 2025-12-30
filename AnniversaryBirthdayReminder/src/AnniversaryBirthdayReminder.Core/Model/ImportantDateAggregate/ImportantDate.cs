// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents an important date to track (birthday, anniversary, or custom occasion).
/// </summary>
public class ImportantDate
{
    /// <summary>
    /// Gets or sets the unique identifier for the important date.
    /// </summary>
    public Guid ImportantDateId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this date.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the person or event.
    /// </summary>
    public string PersonName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of date (Birthday, Anniversary, Custom).
    /// </summary>
    public DateType DateType { get; set; }

    /// <summary>
    /// Gets or sets the actual date value.
    /// </summary>
    public DateTime DateValue { get; set; }

    /// <summary>
    /// Gets or sets the recurrence pattern.
    /// </summary>
    public RecurrencePattern RecurrencePattern { get; set; }

    /// <summary>
    /// Gets or sets the relationship to the person (e.g., Family, Friend, Colleague).
    /// </summary>
    public string? Relationship { get; set; }

    /// <summary>
    /// Gets or sets optional notes about this date.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the date is actively tracked.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of reminders for this date.
    /// </summary>
    public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    /// <summary>
    /// Gets or sets the collection of gifts for this date.
    /// </summary>
    public ICollection<Gift> Gifts { get; set; } = new List<Gift>();

    /// <summary>
    /// Gets or sets the collection of celebrations for this date.
    /// </summary>
    public ICollection<Celebration> Celebrations { get; set; } = new List<Celebration>();

    /// <summary>
    /// Calculates the next occurrence of this date.
    /// </summary>
    /// <returns>The next occurrence date.</returns>
    public DateTime GetNextOccurrence()
    {
        var today = DateTime.UtcNow.Date;
        var thisYear = new DateTime(today.Year, DateValue.Month, DateValue.Day);

        if (thisYear >= today)
        {
            return thisYear;
        }

        return thisYear.AddYears(1);
    }

    /// <summary>
    /// Checks if this date has any pending gifts (gifts with status Idea).
    /// </summary>
    /// <returns>True if there are pending gifts; otherwise, false.</returns>
    public bool HasPendingGifts()
    {
        return Gifts.Any(g => g.Status == GiftStatus.Idea);
    }

    /// <summary>
    /// Toggles the active status of this date.
    /// </summary>
    public void ToggleActive()
    {
        IsActive = !IsActive;
    }
}
