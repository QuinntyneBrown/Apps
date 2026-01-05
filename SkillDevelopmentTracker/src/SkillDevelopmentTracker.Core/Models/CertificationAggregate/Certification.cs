// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Represents a professional certification.
/// </summary>
public class Certification
{
    /// <summary>
    /// Gets or sets the unique identifier for the certification.
    /// </summary>
    public Guid CertificationId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this certification.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the certification.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the issuing organization.
    /// </summary>
    public string IssuingOrganization { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the issue date.
    /// </summary>
    public DateTime IssueDate { get; set; }

    /// <summary>
    /// Gets or sets the expiration date (if applicable).
    /// </summary>
    public DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// Gets or sets the credential ID or certificate number.
    /// </summary>
    public string? CredentialId { get; set; }

    /// <summary>
    /// Gets or sets the credential URL for verification.
    /// </summary>
    public string? CredentialUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the certification is currently valid.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the collection of skill IDs this certification validates.
    /// </summary>
    public List<Guid> SkillIds { get; set; } = new List<Guid>();

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
    /// Checks if the certification is expired.
    /// </summary>
    /// <returns>True if expired; otherwise, false.</returns>
    public bool IsExpired()
    {
        return ExpirationDate.HasValue && ExpirationDate.Value < DateTime.UtcNow;
    }

    /// <summary>
    /// Renews the certification with a new expiration date.
    /// </summary>
    /// <param name="newExpirationDate">The new expiration date.</param>
    public void Renew(DateTime newExpirationDate)
    {
        ExpirationDate = newExpirationDate;
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
