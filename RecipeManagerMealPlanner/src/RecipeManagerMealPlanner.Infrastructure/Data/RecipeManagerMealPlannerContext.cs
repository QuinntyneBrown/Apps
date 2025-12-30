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
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecipeManagerMealPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public RecipeManagerMealPlannerContext(DbContextOptions<RecipeManagerMealPlannerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
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

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Recipe>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<MealPlan>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Ingredient>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ShoppingList>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecipeManagerMealPlannerContext).Assembly);
    }
}
