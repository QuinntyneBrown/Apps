// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;

namespace FreelanceProjectManager.Api.Features.Projects;

/// <summary>
/// Data transfer object for Project.
/// </summary>
public class ProjectDto
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
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
