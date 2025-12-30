// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core;

/// <summary>
/// Represents a shopping list generated from meal plans.
/// </summary>
public class ShoppingList
{
    /// <summary>
    /// Gets or sets the unique identifier for the shopping list.
    /// </summary>
    public Guid ShoppingListId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this shopping list.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the shopping list.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the items in the shopping list (JSON or delimited string).
    /// </summary>
    public string Items { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the list has been completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets notes about the shopping list.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Marks the shopping list as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletedDate = DateTime.UtcNow;
    }
}
