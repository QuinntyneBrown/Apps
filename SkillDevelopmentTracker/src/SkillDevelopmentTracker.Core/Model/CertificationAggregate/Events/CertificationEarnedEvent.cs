// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Event raised when a certification is earned.
/// </summary>
public record CertificationEarnedEvent
{
    /// <summary>
    /// Gets the certification ID.
    /// </summary>
    public Guid CertificationId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the certification name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the issuing organization.
    /// </summary>
    public string IssuingOrganization { get; init; } = string.Empty;

    /// <summary>
    /// Gets the issue date.
    /// </summary>
    public DateTime IssueDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
