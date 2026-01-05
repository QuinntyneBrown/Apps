// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalMissionStatementBuilder.Core;

/// <summary>
/// Represents a personal value.
/// </summary>
public class Value
{
    /// <summary>
    /// Gets or sets the unique identifier for the value.
    /// </summary>
    public Guid ValueId { get; set; }

    /// <summary>
    /// Gets or sets the mission statement ID this value belongs to.
    /// </summary>
    public Guid? MissionStatementId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this value.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the value.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description or explanation of this value.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the priority or rank (lower number = higher priority).
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the mission statement this value belongs to.
    /// </summary>
    public MissionStatement? MissionStatement { get; set; }

    /// <summary>
    /// Updates the priority.
    /// </summary>
    /// <param name="newPriority">The new priority.</param>
    public void UpdatePriority(int newPriority)
    {
        Priority = newPriority;
        UpdatedAt = DateTime.UtcNow;
    }
}
