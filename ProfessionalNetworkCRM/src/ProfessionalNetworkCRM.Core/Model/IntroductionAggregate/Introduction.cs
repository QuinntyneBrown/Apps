// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ProfessionalNetworkCRM.Core.Model.IntroductionAggregate.Enums;

namespace ProfessionalNetworkCRM.Core.Model.IntroductionAggregate;

/// <summary>
/// Represents a professional introduction between contacts.
/// </summary>
public class Introduction
{
    /// <summary>
    /// Gets or sets the unique identifier for the introduction.
    /// </summary>
    public Guid IntroductionId { get; set; }

    /// <summary>
    /// Gets or sets the contact ID requesting the introduction.
    /// </summary>
    public Guid FromContactId { get; set; }

    /// <summary>
    /// Gets or sets the contact ID to whom the introduction is made.
    /// </summary>
    public Guid ToContactId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the purpose of the introduction.
    /// </summary>
    public string Purpose { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the introduction.
    /// </summary>
    public IntroductionStatus Status { get; set; }

    /// <summary>
    /// Gets or sets optional notes about this introduction.
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
    /// Updates the status of the introduction.
    /// </summary>
    /// <param name="newStatus">The new status.</param>
    public void UpdateStatus(IntroductionStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}
