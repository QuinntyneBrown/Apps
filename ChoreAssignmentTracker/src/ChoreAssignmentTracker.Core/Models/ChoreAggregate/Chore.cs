// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Represents a household chore.
/// </summary>
public class Chore
{
    /// <summary>
    /// Gets or sets the unique identifier for the chore.
    /// </summary>
    public Guid ChoreId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this chore.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the chore.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the chore.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the frequency of the chore.
    /// </summary>
    public ChoreFrequency Frequency { get; set; }

    /// <summary>
    /// Gets or sets the estimated time in minutes.
    /// </summary>
    public int? EstimatedMinutes { get; set; }

    /// <summary>
    /// Gets or sets the point value for completing the chore.
    /// </summary>
    public int Points { get; set; }

    /// <summary>
    /// Gets or sets the category or area.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the chore is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of assignments for this chore.
    /// </summary>
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    /// <summary>
    /// Calculates the next due date based on the frequency.
    /// </summary>
    /// <param name="fromDate">The starting date.</param>
    /// <returns>The next due date.</returns>
    public DateTime CalculateNextDueDate(DateTime fromDate)
    {
        return Frequency switch
        {
            ChoreFrequency.Daily => fromDate.AddDays(1),
            ChoreFrequency.Weekly => fromDate.AddDays(7),
            ChoreFrequency.BiWeekly => fromDate.AddDays(14),
            ChoreFrequency.Monthly => fromDate.AddMonths(1),
            ChoreFrequency.AsNeeded => fromDate,
            _ => fromDate,
        };
    }
}
