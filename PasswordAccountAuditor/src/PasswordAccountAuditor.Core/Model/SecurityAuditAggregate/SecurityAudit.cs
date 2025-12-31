// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Represents a security audit performed on an account.
/// </summary>
public class SecurityAudit
{
    /// <summary>
    /// Gets or sets the unique identifier for the security audit.
    /// </summary>
    public Guid SecurityAuditId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the account being audited.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Gets or sets the audit type.
    /// </summary>
    public AuditType AuditType { get; set; }

    /// <summary>
    /// Gets or sets the result status of the audit.
    /// </summary>
    public AuditStatus Status { get; set; } = AuditStatus.Pending;

    /// <summary>
    /// Gets or sets the date and time when the audit was performed.
    /// </summary>
    public DateTime AuditDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the findings from the audit.
    /// </summary>
    public string? Findings { get; set; }

    /// <summary>
    /// Gets or sets the recommendations based on the audit.
    /// </summary>
    public string? Recommendations { get; set; }

    /// <summary>
    /// Gets or sets the security score (0-100).
    /// </summary>
    public int SecurityScore { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the audit.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the account.
    /// </summary>
    public Account? Account { get; set; }

    /// <summary>
    /// Marks the audit as completed with findings.
    /// </summary>
    /// <param name="findings">The audit findings.</param>
    /// <param name="score">The security score.</param>
    /// <param name="recommendations">Optional recommendations.</param>
    public void Complete(string findings, int score, string? recommendations = null)
    {
        Status = AuditStatus.Completed;
        Findings = findings;
        SecurityScore = Math.Clamp(score, 0, 100);
        Recommendations = recommendations;
    }

    /// <summary>
    /// Marks the audit as failed.
    /// </summary>
    /// <param name="reason">The reason for failure.</param>
    public void MarkAsFailed(string reason)
    {
        Status = AuditStatus.Failed;
        Notes = reason;
    }

    /// <summary>
    /// Checks if the audit indicates critical security issues.
    /// </summary>
    /// <returns>True if security score is below 40; otherwise, false.</returns>
    public bool HasCriticalIssues()
    {
        return SecurityScore < 40;
    }

    /// <summary>
    /// Gets the risk level based on the security score.
    /// </summary>
    /// <returns>The risk level description.</returns>
    public string GetRiskLevel()
    {
        return SecurityScore switch
        {
            >= 80 => "Low Risk",
            >= 60 => "Medium Risk",
            >= 40 => "High Risk",
            _ => "Critical Risk"
        };
    }
}
