// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Core;

/// <summary>
/// Represents a freelance project.
/// </summary>
public class Project
{
    /// <summary>
    /// Gets or sets the unique identifier for the project.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this project.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Gets or sets the project name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the project description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the project status.
    /// </summary>
    public ProjectStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// Gets or sets the hourly rate.
    /// </summary>
    public decimal? HourlyRate { get; set; }

    /// <summary>
    /// Gets or sets the fixed project budget.
    /// </summary>
    public decimal? FixedBudget { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    public string Currency { get; set; } = "USD";

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
    /// Gets or sets the navigation property to the client.
    /// </summary>
    public Client? Client { get; set; }

    /// <summary>
    /// Gets or sets the collection of time entries.
    /// </summary>
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();

    /// <summary>
    /// Gets or sets the collection of invoices.
    /// </summary>
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    /// <summary>
    /// Updates the project status.
    /// </summary>
    /// <param name="newStatus">The new status.</param>
    public void UpdateStatus(ProjectStatus newStatus)
    {
        Status = newStatus;
        if (newStatus == ProjectStatus.Completed)
        {
            CompletionDate = DateTime.UtcNow;
        }
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Calculates total hours worked on the project.
    /// </summary>
    /// <returns>Total hours.</returns>
    public decimal GetTotalHours()
    {
        return TimeEntries.Sum(te => te.Hours);
    }
}
