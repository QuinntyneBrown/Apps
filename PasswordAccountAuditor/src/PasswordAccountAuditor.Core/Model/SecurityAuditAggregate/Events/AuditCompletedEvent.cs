// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Event raised when a security audit is completed.
/// </summary>
public record AuditCompletedEvent
{
    /// <summary>
    /// Gets the security audit ID.
    /// </summary>
    public Guid SecurityAuditId { get; init; }

    /// <summary>
    /// Gets the account ID.
    /// </summary>
    public Guid AccountId { get; init; }

    /// <summary>
    /// Gets the security score.
    /// </summary>
    public int SecurityScore { get; init; }

    /// <summary>
    /// Gets a value indicating whether critical issues were found.
    /// </summary>
    public bool HasCriticalIssues { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
