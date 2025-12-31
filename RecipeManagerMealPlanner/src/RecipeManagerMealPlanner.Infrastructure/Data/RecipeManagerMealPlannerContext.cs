// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RecipeManagerMealPlanner.Core;
using Microsoft.EntityFrameworkCore;

using RecipeManagerMealPlanner.Core.Model.UserAggregate;
using RecipeManagerMealPlanner.Core.Model.UserAggregate.Entities;
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


    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    public DbSet<Role> Roles { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // Apply tenant filter to User
        modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);

        // Apply tenant filter to Role
        modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);

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
