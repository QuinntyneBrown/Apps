// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using RecipeManagerMealPlanner.Core.Models.UserAggregate;
using RecipeManagerMealPlanner.Core.Models.UserAggregate.Entities;
namespace RecipeManagerMealPlanner.Core;

/// <summary>
/// Represents the persistence surface for the RecipeManagerMealPlanner system.
/// </summary>
public interface IRecipeManagerMealPlannerContext
{
    /// <summary>
    /// Gets or sets the DbSet of recipes.
    /// </summary>
    DbSet<Recipe> Recipes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of meal plans.
    /// </summary>
    DbSet<MealPlan> MealPlans { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of ingredients.
    /// </summary>
    DbSet<Ingredient> Ingredients { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of shopping lists.
    /// </summary>
    DbSet<ShoppingList> ShoppingLists { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    
    /// <summary>
    /// Gets the users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Gets the roles.
    /// </summary>
    DbSet<Role> Roles { get; }

    /// <summary>
    /// Gets the user roles.
    /// </summary>
    DbSet<UserRole> UserRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
