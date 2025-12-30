// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MealPrepPlanner.Core;
using Microsoft.EntityFrameworkCore;

namespace MealPrepPlanner.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MealPrepPlanner system.
/// </summary>
public class MealPrepPlannerContext : DbContext, IMealPrepPlannerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MealPrepPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MealPrepPlannerContext(DbContextOptions<MealPrepPlannerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<MealPlan> MealPlans { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Recipe> Recipes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<GroceryList> GroceryLists { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Nutrition> Nutritions { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MealPrepPlannerContext).Assembly);
    }
}
