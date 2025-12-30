// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core;

/// <summary>
/// Event raised when a new medication is added.
/// </summary>
public record MedicationAddedEvent
{
    /// <summary>
    /// Gets the medication ID.
    /// </summary>
    public Guid MedicationId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the medication name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the medication type.
    /// </summary>
    public MedicationType MedicationType { get; init; }

    /// <summary>
    /// Gets the dosage.
    /// </summary>
    public string Dosage { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
