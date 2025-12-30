// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DateNightIdeaGenerator.Core;

/// <summary>
/// Event raised when a new date idea is created.
/// </summary>
public record DateIdeaCreatedEvent
{
    /// <summary>
    /// Gets the date idea ID.
    /// </summary>
    public Guid DateIdeaId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the category.
    /// </summary>
    public Category Category { get; init; }

    /// <summary>
    /// Gets the budget range.
    /// </summary>
    public BudgetRange BudgetRange { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
