// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SalaryCompensationTracker.Core;

/// <summary>
/// Represents an employment benefit.
/// </summary>
public class Benefit
{
    /// <summary>
    /// Gets or sets the unique identifier for the benefit.
    /// </summary>
    public Guid BenefitId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this benefit.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the compensation ID this benefit is associated with.
    /// </summary>
    public Guid? CompensationId { get; set; }

    /// <summary>
    /// Gets or sets the benefit name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the benefit category (Health, Retirement, PTO, etc.).
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the benefit.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the estimated annual value.
    /// </summary>
    public decimal? EstimatedValue { get; set; }

    /// <summary>
    /// Gets or sets the employer contribution amount.
    /// </summary>
    public decimal? EmployerContribution { get; set; }

    /// <summary>
    /// Gets or sets the employee contribution amount.
    /// </summary>
    public decimal? EmployeeContribution { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the compensation.
    /// </summary>
    public Compensation? Compensation { get; set; }
}
