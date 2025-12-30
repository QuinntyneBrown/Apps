// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalMissionStatementBuilder.Core;

/// <summary>
/// Represents a personal mission statement.
/// </summary>
public class MissionStatement
{
    /// <summary>
    /// Gets or sets the unique identifier for the mission statement.
    /// </summary>
    public Guid MissionStatementId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this mission statement.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the title of the mission statement.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the text of the mission statement.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version number.
    /// </summary>
    public int Version { get; set; } = 1;

    /// <summary>
    /// Gets or sets a value indicating whether this is the current active version.
    /// </summary>
    public bool IsCurrentVersion { get; set; } = true;

    /// <summary>
    /// Gets or sets the date the mission statement was created.
    /// </summary>
    public DateTime StatementDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of values associated with this mission statement.
    /// </summary>
    public ICollection<Value> Values { get; set; } = new List<Value>();

    /// <summary>
    /// Gets or sets the collection of goals associated with this mission statement.
    /// </summary>
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();

    /// <summary>
    /// Creates a new version of the mission statement.
    /// </summary>
    public void CreateNewVersion()
    {
        IsCurrentVersion = false;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the text of the mission statement.
    /// </summary>
    /// <param name="newText">The new text.</param>
    public void UpdateText(string newText)
    {
        Text = newText;
        UpdatedAt = DateTime.UtcNow;
    }
}
