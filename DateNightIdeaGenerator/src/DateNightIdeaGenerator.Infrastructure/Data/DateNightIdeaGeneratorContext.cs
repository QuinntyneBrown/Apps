// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using Microsoft.EntityFrameworkCore;

using DateNightIdeaGenerator.Core.Model.UserAggregate;
using DateNightIdeaGenerator.Core.Model.UserAggregate.Entities;
namespace DateNightIdeaGenerator.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the DateNightIdeaGenerator system.
/// </summary>
public class DateNightIdeaGeneratorContext : DbContext, IDateNightIdeaGeneratorContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="DateNightIdeaGeneratorContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public DateNightIdeaGeneratorContext(DbContextOptions<DateNightIdeaGeneratorContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<DateIdea> DateIdeas { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Experience> Experiences { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Rating> Ratings { get; set; } = null!;


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
            modelBuilder.Entity<DateIdea>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Experience>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Rating>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DateNightIdeaGeneratorContext).Assembly);
    }
}
