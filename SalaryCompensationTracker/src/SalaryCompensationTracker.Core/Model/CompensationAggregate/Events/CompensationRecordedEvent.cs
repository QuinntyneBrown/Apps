// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SalaryCompensationTracker.Core;

/// <summary>
/// Event raised when a compensation record is created.
/// </summary>
public record CompensationRecordedEvent
{
    /// <summary>
    /// Gets the compensation ID.
    /// </summary>
    public Guid CompensationId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the employer.
    /// </summary>
    public string Employer { get; init; } = string.Empty;

    /// <summary>
    /// Gets the total compensation.
    /// </summary>
    public decimal TotalCompensation { get; init; }

    /// <summary>
    /// Gets the effective date.
    /// </summary>
    public DateTime EffectiveDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
