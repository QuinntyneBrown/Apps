// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core;

/// <summary>
/// Represents a job offer.
/// </summary>
public class Offer
{
    /// <summary>
    /// Gets or sets the unique identifier for the offer.
    /// </summary>
    public Guid OfferId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this offer.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the application ID.
    /// </summary>
    public Guid ApplicationId { get; set; }

    /// <summary>
    /// Gets or sets the offered salary.
    /// </summary>
    public decimal Salary { get; set; }

    /// <summary>
    /// Gets or sets the salary currency (USD, EUR, etc.).
    /// </summary>
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Gets or sets the bonus or signing bonus amount.
    /// </summary>
    public decimal? Bonus { get; set; }

    /// <summary>
    /// Gets or sets the equity or stock options details.
    /// </summary>
    public string? Equity { get; set; }

    /// <summary>
    /// Gets or sets the benefits summary.
    /// </summary>
    public string? Benefits { get; set; }

    /// <summary>
    /// Gets or sets the vacation days per year.
    /// </summary>
    public int? VacationDays { get; set; }

    /// <summary>
    /// Gets or sets the offer date.
    /// </summary>
    public DateTime OfferDate { get; set; }

    /// <summary>
    /// Gets or sets the expiration or deadline date.
    /// </summary>
    public DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the offer is accepted.
    /// </summary>
    public bool IsAccepted { get; set; }

    /// <summary>
    /// Gets or sets the decision date.
    /// </summary>
    public DateTime? DecisionDate { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the application.
    /// </summary>
    public Application? Application { get; set; }

    /// <summary>
    /// Accepts the offer.
    /// </summary>
    public void Accept()
    {
        IsAccepted = true;
        DecisionDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Declines the offer.
    /// </summary>
    public void Decline()
    {
        IsAccepted = false;
        DecisionDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the offer has expired.
    /// </summary>
    /// <returns>True if expired; otherwise, false.</returns>
    public bool IsExpired()
    {
        return ExpirationDate.HasValue && ExpirationDate.Value < DateTime.UtcNow && DecisionDate == null;
    }
}
