// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Event raised when a security audit is scheduled.
/// </summary>
public record AuditScheduledEvent
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
    /// Gets the audit type.
    /// </summary>
    public AuditType AuditType { get; init; }

    /// <summary>
    /// Gets the scheduled audit date.
    /// </summary>
    public DateTime AuditDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
