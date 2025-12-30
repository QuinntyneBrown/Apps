// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core;

/// <summary>
/// Event raised when a job application is submitted.
/// </summary>
public record ApplicationSubmittedEvent
{
    /// <summary>
    /// Gets the application ID.
    /// </summary>
    public Guid ApplicationId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the job title.
    /// </summary>
    public string JobTitle { get; init; } = string.Empty;

    /// <summary>
    /// Gets the company ID.
    /// </summary>
    public Guid CompanyId { get; init; }

    /// <summary>
    /// Gets the applied date.
    /// </summary>
    public DateTime AppliedDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
