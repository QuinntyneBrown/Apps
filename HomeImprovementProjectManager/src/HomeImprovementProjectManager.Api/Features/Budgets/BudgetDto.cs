// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Data transfer object for Budget.
/// </summary>
public record BudgetDto
{
    /// <summary>
    /// Gets or sets the budget ID.
    /// </summary>
    public Guid BudgetId { get; init; }

    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the allocated amount.
    /// </summary>
    public decimal AllocatedAmount { get; init; }

    /// <summary>
    /// Gets or sets the spent amount.
    /// </summary>
    public decimal? SpentAmount { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Budget.
/// </summary>
public static class BudgetExtensions
{
    /// <summary>
    /// Converts a Budget to a DTO.
    /// </summary>
    /// <param name="budget">The budget.</param>
    /// <returns>The DTO.</returns>
    public static BudgetDto ToDto(this Budget budget)
    {
        return new BudgetDto
        {
            BudgetId = budget.BudgetId,
            ProjectId = budget.ProjectId,
            Category = budget.Category,
            AllocatedAmount = budget.AllocatedAmount,
            SpentAmount = budget.SpentAmount,
            CreatedAt = budget.CreatedAt,
        };
    }
}
