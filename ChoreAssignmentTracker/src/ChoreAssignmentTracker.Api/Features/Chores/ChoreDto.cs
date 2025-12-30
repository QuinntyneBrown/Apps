// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;

namespace ChoreAssignmentTracker.Api.Features.Chores;

/// <summary>
/// Represents a chore data transfer object.
/// </summary>
public class ChoreDto
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
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Represents a request to create a chore.
/// </summary>
public class CreateChoreDto
{
    /// <summary>
    /// Gets or sets the user ID who owns this chore.
    /// </summary>
    public Guid UserId { get; set; }

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
}

/// <summary>
/// Represents a request to update a chore.
/// </summary>
public class UpdateChoreDto
{
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
    public bool IsActive { get; set; }
}
