// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DailyJournalingApp.Core;

/// <summary>
/// Represents a tag for categorizing journal entries.
/// </summary>
public class Tag
{
    /// <summary>
    /// Gets or sets the unique identifier for the tag.
    /// </summary>
    public Guid TagId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this tag.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the tag.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the color code for the tag (e.g., "#FF5733").
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the usage count.
    /// </summary>
    public int UsageCount { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Increments the usage count.
    /// </summary>
    public void IncrementUsageCount()
    {
        UsageCount++;
    }
}
