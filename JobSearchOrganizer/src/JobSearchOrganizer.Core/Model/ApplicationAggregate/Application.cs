// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core;

/// <summary>
/// Represents a job application.
/// </summary>
public class Application
{
    /// <summary>
    /// Gets or sets the unique identifier for the application.
    /// </summary>
    public Guid ApplicationId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this application.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the company ID.
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// Gets or sets the job title.
    /// </summary>
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the job posting URL.
    /// </summary>
    public string? JobUrl { get; set; }

    /// <summary>
    /// Gets or sets the application status.
    /// </summary>
    public ApplicationStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the date applied.
    /// </summary>
    public DateTime AppliedDate { get; set; }

    /// <summary>
    /// Gets or sets the salary range or expectation.
    /// </summary>
    public string? SalaryRange { get; set; }

    /// <summary>
    /// Gets or sets the location of the job.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the job type (Full-time, Part-time, Contract, etc.).
    /// </summary>
    public string? JobType { get; set; }

    /// <summary>
    /// Gets or sets whether the job is remote.
    /// </summary>
    public bool IsRemote { get; set; }

    /// <summary>
    /// Gets or sets the contact person's name.
    /// </summary>
    public string? ContactPerson { get; set; }

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
    /// Gets or sets the navigation property to the company.
    /// </summary>
    public Company? Company { get; set; }

    /// <summary>
    /// Gets or sets the collection of interviews for this application.
    /// </summary>
    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    /// <summary>
    /// Gets or sets the offer for this application.
    /// </summary>
    public Offer? Offer { get; set; }

    /// <summary>
    /// Updates the status of the application.
    /// </summary>
    /// <param name="newStatus">The new status.</param>
    public void UpdateStatus(ApplicationStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the application is in a final state.
    /// </summary>
    /// <returns>True if in a final state; otherwise, false.</returns>
    public bool IsInFinalState()
    {
        return Status == ApplicationStatus.Accepted ||
               Status == ApplicationStatus.Rejected ||
               Status == ApplicationStatus.Withdrawn;
    }
}
