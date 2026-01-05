// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core;

/// <summary>
/// Event raised when a beneficiary is added.
/// </summary>
public record BeneficiaryAddedEvent
{
    /// <summary>
    /// Gets the beneficiary ID.
    /// </summary>
    public Guid BeneficiaryId { get; init; }

    /// <summary>
    /// Gets the plan ID.
    /// </summary>
    public Guid PlanId { get; init; }

    /// <summary>
    /// Gets the beneficiary's first name.
    /// </summary>
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the beneficiary's last name.
    /// </summary>
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
