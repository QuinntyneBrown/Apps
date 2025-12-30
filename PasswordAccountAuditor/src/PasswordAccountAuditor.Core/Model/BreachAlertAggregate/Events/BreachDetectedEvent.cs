// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Event raised when a security breach is detected.
/// </summary>
public record BreachDetectedEvent
{
    /// <summary>
    /// Gets the breach alert ID.
    /// </summary>
    public Guid BreachAlertId { get; init; }

    /// <summary>
    /// Gets the account ID.
    /// </summary>
    public Guid AccountId { get; init; }

    /// <summary>
    /// Gets the breach severity.
    /// </summary>
    public BreachSeverity Severity { get; init; }

    /// <summary>
    /// Gets the breach description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the breach was detected.
    /// </summary>
    public DateTime DetectedDate { get; init; } = DateTime.UtcNow;
}
