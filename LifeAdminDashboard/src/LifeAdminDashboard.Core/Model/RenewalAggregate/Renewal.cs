// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core;

/// <summary>
/// Represents a renewal (subscription, license, insurance, etc.).
/// </summary>
public class Renewal
{
    /// <summary>
    /// Gets or sets the unique identifier for the renewal.
    /// </summary>
    public Guid RenewalId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this renewal.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the renewal name/title.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the renewal type (subscription, license, insurance, etc.).
    /// </summary>
    public string RenewalType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the provider or company.
    /// </summary>
    public string? Provider { get; set; }

    /// <summary>
    /// Gets or sets the renewal date.
    /// </summary>
    public DateTime RenewalDate { get; set; }

    /// <summary>
    /// Gets or sets the cost/amount.
    /// </summary>
    public decimal? Cost { get; set; }

    /// <summary>
    /// Gets or sets the frequency (monthly, yearly, etc.).
    /// </summary>
    public string Frequency { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether auto-renewal is enabled.
    /// </summary>
    public bool IsAutoRenewal { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the renewal is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if the renewal is coming up soon (within 30 days).
    /// </summary>
    /// <returns>True if due soon; otherwise, false.</returns>
    public bool IsDueSoon()
    {
        return IsActive && (RenewalDate - DateTime.UtcNow).Days <= 30;
    }

    /// <summary>
    /// Cancels the renewal.
    /// </summary>
    public void Cancel()
    {
        IsActive = false;
    }

    /// <summary>
    /// Renews and updates the next renewal date.
    /// </summary>
    public void Renew()
    {
        if (Frequency.ToLower().Contains("month"))
        {
            RenewalDate = RenewalDate.AddMonths(1);
        }
        else if (Frequency.ToLower().Contains("year"))
        {
            RenewalDate = RenewalDate.AddYears(1);
        }
    }
}
