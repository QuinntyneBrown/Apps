// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core;

/// <summary>
/// Represents a saved search query for the knowledge base.
/// </summary>
public class SearchQuery
{
    /// <summary>
    /// Gets or sets the unique identifier for the search query.
    /// </summary>
    public Guid SearchQueryId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this search query.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the query text.
    /// </summary>
    public string QueryText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the query name or description.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a saved/favorite query.
    /// </summary>
    public bool IsSaved { get; set; }

    /// <summary>
    /// Gets or sets the number of times this query has been executed.
    /// </summary>
    public int ExecutionCount { get; set; }

    /// <summary>
    /// Gets or sets the last execution timestamp.
    /// </summary>
    public DateTime? LastExecutedAt { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Executes the query and increments the execution count.
    /// </summary>
    public void Execute()
    {
        ExecutionCount++;
        LastExecutedAt = DateTime.UtcNow;
    }
}
