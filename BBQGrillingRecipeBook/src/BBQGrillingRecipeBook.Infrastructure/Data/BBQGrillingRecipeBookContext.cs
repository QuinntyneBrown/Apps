// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using Microsoft.EntityFrameworkCore;

using BBQGrillingRecipeBook.Core.Model.UserAggregate;
using BBQGrillingRecipeBook.Core.Model.UserAggregate.Entities;
namespace BBQGrillingRecipeBook.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the BBQGrillingRecipeBook system.
/// </summary>
public class BBQGrillingRecipeBookContext : DbContext, IBBQGrillingRecipeBookContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="BBQGrillingRecipeBookContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public BBQGrillingRecipeBookContext(DbContextOptions<BBQGrillingRecipeBookContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Recipe> Recipes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<CookSession> CookSessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Technique> Techniques { get; set; } = null!;


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
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Recipe>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<CookSession>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Technique>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BBQGrillingRecipeBookContext).Assembly);
    }
}
