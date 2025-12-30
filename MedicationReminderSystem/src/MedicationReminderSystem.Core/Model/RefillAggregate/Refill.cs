// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core;

/// <summary>
/// Represents a medication refill record.
/// </summary>
public class Refill
{
    /// <summary>
    /// Gets or sets the unique identifier for the refill.
    /// </summary>
    public Guid RefillId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this refill.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the medication ID.
    /// </summary>
    public Guid MedicationId { get; set; }

    /// <summary>
    /// Gets or sets the refill date.
    /// </summary>
    public DateTime RefillDate { get; set; }

    /// <summary>
    /// Gets or sets the quantity refilled.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the pharmacy name.
    /// </summary>
    public string? PharmacyName { get; set; }

    /// <summary>
    /// Gets or sets the cost of the refill.
    /// </summary>
    public decimal? Cost { get; set; }

    /// <summary>
    /// Gets or sets the next refill due date.
    /// </summary>
    public DateTime? NextRefillDate { get; set; }

    /// <summary>
    /// Gets or sets the number of refills remaining.
    /// </summary>
    public int? RefillsRemaining { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the associated medication.
    /// </summary>
    public Medication? Medication { get; set; }

    /// <summary>
    /// Checks if a refill is due soon (within next 7 days).
    /// </summary>
    /// <returns>True if due soon; otherwise, false.</returns>
    public bool IsRefillDueSoon()
    {
        if (!NextRefillDate.HasValue) return false;
        return NextRefillDate.Value <= DateTime.UtcNow.AddDays(7);
    }

    /// <summary>
    /// Checks if there are no refills remaining.
    /// </summary>
    /// <returns>True if no refills remaining; otherwise, false.</returns>
    public bool NoRefillsRemaining()
    {
        return RefillsRemaining.HasValue && RefillsRemaining.Value == 0;
    }
}
