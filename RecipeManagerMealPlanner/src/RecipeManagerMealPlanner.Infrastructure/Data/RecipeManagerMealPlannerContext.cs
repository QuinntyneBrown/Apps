// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RecipeManagerMealPlanner.Core;
using Microsoft.EntityFrameworkCore;

namespace RecipeManagerMealPlanner.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the RecipeManagerMealPlanner system.
/// </summary>
public class RecipeManagerMealPlannerContext : DbContext, IRecipeManagerMealPlannerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RecipeManagerMealPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public RecipeManagerMealPlannerContext(DbContextOptions<RecipeManagerMealPlannerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Recipe> Recipes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<MealPlan> MealPlans { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Ingredient> Ingredients { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ShoppingList> ShoppingLists { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecipeManagerMealPlannerContext).Assembly);
    }
}
