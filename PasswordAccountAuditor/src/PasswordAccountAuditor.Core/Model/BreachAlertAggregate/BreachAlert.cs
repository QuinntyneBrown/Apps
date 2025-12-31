// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Represents a security breach alert for an account.
/// </summary>
public class BreachAlert
{
    /// <summary>
    /// Gets or sets the unique identifier for the breach alert.
    /// </summary>
    public Guid BreachAlertId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the account affected by the breach.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Gets or sets the severity level of the breach.
    /// </summary>
    public BreachSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the status of the alert.
    /// </summary>
    public AlertStatus Status { get; set; } = AlertStatus.New;

    /// <summary>
    /// Gets or sets the date when the breach was detected.
    /// </summary>
    public DateTime DetectedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date when the breach occurred (if known).
    /// </summary>
    public DateTime? BreachDate { get; set; }

    /// <summary>
    /// Gets or sets the source of the breach information.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the description of the breach.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the types of data compromised in the breach.
    /// </summary>
    public string? DataCompromised { get; set; }

    /// <summary>
    /// Gets or sets the recommended actions to take.
    /// </summary>
    public string? RecommendedActions { get; set; }

    /// <summary>
    /// Gets or sets the date when the alert was acknowledged.
    /// </summary>
    public DateTime? AcknowledgedAt { get; set; }

    /// <summary>
    /// Gets or sets the date when the issue was resolved.
    /// </summary>
    public DateTime? ResolvedAt { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the breach alert.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the account.
    /// </summary>
    public Account? Account { get; set; }

    /// <summary>
    /// Acknowledges the breach alert.
    /// </summary>
    public void Acknowledge()
    {
        Status = AlertStatus.Acknowledged;
        AcknowledgedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the breach alert as resolved.
    /// </summary>
    /// <param name="resolutionNotes">Optional notes about the resolution.</param>
    public void Resolve(string? resolutionNotes = null)
    {
        Status = AlertStatus.Resolved;
        ResolvedAt = DateTime.UtcNow;
        if (!string.IsNullOrEmpty(resolutionNotes))
        {
            Notes = string.IsNullOrEmpty(Notes)
                ? resolutionNotes
                : $"{Notes}\n\nResolution: {resolutionNotes}";
        }
    }

    /// <summary>
    /// Dismisses the breach alert as a false positive.
    /// </summary>
    /// <param name="reason">The reason for dismissal.</param>
    public void Dismiss(string reason)
    {
        Status = AlertStatus.Dismissed;
        Notes = string.IsNullOrEmpty(Notes)
            ? $"Dismissed: {reason}"
            : $"{Notes}\n\nDismissed: {reason}";
    }

    /// <summary>
    /// Checks if the alert requires immediate action based on severity.
    /// </summary>
    /// <returns>True if severity is critical or high; otherwise, false.</returns>
    public bool RequiresImmediateAction()
    {
        return Severity == BreachSeverity.Critical || Severity == BreachSeverity.High;
    }

    /// <summary>
    /// Gets the number of days since the breach was detected.
    /// </summary>
    /// <returns>The number of days since detection.</returns>
    public int DaysSinceDetection()
    {
        return (DateTime.UtcNow - DetectedDate).Days;
    }
}
