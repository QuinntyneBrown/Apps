// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Represents an online account that needs security monitoring.
/// </summary>
public class Account
{
    /// <summary>
    /// Gets or sets the unique identifier for the account.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this account.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the account name or service name.
    /// </summary>
    public string AccountName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username or email used for this account.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the website URL for the account.
    /// </summary>
    public string? WebsiteUrl { get; set; }

    /// <summary>
    /// Gets or sets the category of the account.
    /// </summary>
    public AccountCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the current security level of the account.
    /// </summary>
    public SecurityLevel SecurityLevel { get; set; } = SecurityLevel.Unknown;

    /// <summary>
    /// Gets or sets a value indicating whether two-factor authentication is enabled.
    /// </summary>
    public bool HasTwoFactorAuth { get; set; }

    /// <summary>
    /// Gets or sets the date when the password was last changed.
    /// </summary>
    public DateTime? LastPasswordChange { get; set; }

    /// <summary>
    /// Gets or sets the date when the account was last accessed.
    /// </summary>
    public DateTime? LastAccessDate { get; set; }

    /// <summary>
    /// Gets or sets optional notes about this account.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the account is actively monitored.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of security audits for this account.
    /// </summary>
    public ICollection<SecurityAudit> SecurityAudits { get; set; } = new List<SecurityAudit>();

    /// <summary>
    /// Gets or sets the collection of breach alerts for this account.
    /// </summary>
    public ICollection<BreachAlert> BreachAlerts { get; set; } = new List<BreachAlert>();

    /// <summary>
    /// Updates the password change timestamp to now.
    /// </summary>
    public void RecordPasswordChange()
    {
        LastPasswordChange = DateTime.UtcNow;
        SecurityLevel = CalculateSecurityLevel();
    }

    /// <summary>
    /// Enables two-factor authentication for this account.
    /// </summary>
    public void EnableTwoFactorAuth()
    {
        HasTwoFactorAuth = true;
        SecurityLevel = CalculateSecurityLevel();
    }

    /// <summary>
    /// Disables two-factor authentication for this account.
    /// </summary>
    public void DisableTwoFactorAuth()
    {
        HasTwoFactorAuth = false;
        SecurityLevel = CalculateSecurityLevel();
    }

    /// <summary>
    /// Updates the last access date to now.
    /// </summary>
    public void RecordAccess()
    {
        LastAccessDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Toggles the active status of this account.
    /// </summary>
    public void ToggleActive()
    {
        IsActive = !IsActive;
    }

    /// <summary>
    /// Checks if the password needs to be changed (older than 90 days).
    /// </summary>
    /// <returns>True if password change is recommended; otherwise, false.</returns>
    public bool NeedsPasswordChange()
    {
        if (!LastPasswordChange.HasValue)
            return true;

        return (DateTime.UtcNow - LastPasswordChange.Value).TotalDays > 90;
    }

    /// <summary>
    /// Calculates the security level based on account settings.
    /// </summary>
    /// <returns>The calculated security level.</returns>
    private SecurityLevel CalculateSecurityLevel()
    {
        if (!HasTwoFactorAuth)
            return SecurityLevel.Low;

        if (NeedsPasswordChange())
            return SecurityLevel.Medium;

        return SecurityLevel.High;
    }
}
