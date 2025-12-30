// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace MealPrepPlanner.Core;

/// <summary>
/// Represents the persistence surface for the MealPrepPlanner system.
/// </summary>
public interface IMealPrepPlannerContext
{
    /// <summary>
    /// Gets or sets the DbSet of meal plans.
    /// </summary>
    DbSet<MealPlan> MealPlans { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of recipes.
    /// </summary>
    DbSet<Recipe> Recipes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of grocery lists.
    /// </summary>
    DbSet<GroceryList> GroceryLists { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of nutrition data.
    /// </summary>
    DbSet<Nutrition> Nutritions { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
