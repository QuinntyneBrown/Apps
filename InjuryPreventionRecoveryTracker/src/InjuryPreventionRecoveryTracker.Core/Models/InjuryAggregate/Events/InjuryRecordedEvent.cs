// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core;

/// <summary>
/// Event raised when a new injury is recorded.
/// </summary>
public record InjuryRecordedEvent
{
    /// <summary>
    /// Gets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the injury type.
    /// </summary>
    public InjuryType InjuryType { get; init; }

    /// <summary>
    /// Gets the injury severity.
    /// </summary>
    public InjurySeverity Severity { get; init; }

    /// <summary>
    /// Gets the body part affected.
    /// </summary>
    public string BodyPart { get; init; } = string.Empty;

    /// <summary>
    /// Gets the injury date.
    /// </summary>
    public DateTime InjuryDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
