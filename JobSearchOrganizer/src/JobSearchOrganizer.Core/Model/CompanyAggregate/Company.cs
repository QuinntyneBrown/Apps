// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core;

/// <summary>
/// Represents a company.
/// </summary>
public class Company
{
    /// <summary>
    /// Gets or sets the unique identifier for the company.
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this company record.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the company name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the industry.
    /// </summary>
    public string? Industry { get; set; }

    /// <summary>
    /// Gets or sets the company website URL.
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Gets or sets the company location.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the company size.
    /// </summary>
    public string? CompanySize { get; set; }

    /// <summary>
    /// Gets or sets the company culture notes.
    /// </summary>
    public string? CultureNotes { get; set; }

    /// <summary>
    /// Gets or sets research notes about the company.
    /// </summary>
    public string? ResearchNotes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this company is a target.
    /// </summary>
    public bool IsTargetCompany { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of applications to this company.
    /// </summary>
    public ICollection<Application> Applications { get; set; } = new List<Application>();

    /// <summary>
    /// Marks the company as a target.
    /// </summary>
    public void MarkAsTarget()
    {
        IsTargetCompany = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
