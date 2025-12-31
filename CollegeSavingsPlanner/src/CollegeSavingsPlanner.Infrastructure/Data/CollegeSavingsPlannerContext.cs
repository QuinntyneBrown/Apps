// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using Microsoft.EntityFrameworkCore;

using CollegeSavingsPlanner.Core.Model.UserAggregate;
using CollegeSavingsPlanner.Core.Model.UserAggregate.Entities;
namespace CollegeSavingsPlanner.Infrastructure.Data;

/// <summary>
/// Represents the Entity Framework database context for the CollegeSavingsPlanner system.
/// </summary>
public class CollegeSavingsPlannerContext : DbContext, ICollegeSavingsPlannerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CollegeSavingsPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public CollegeSavingsPlannerContext(DbContextOptions<CollegeSavingsPlannerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <summary>
    /// Gets or sets the DbSet of 529 plans.
    /// </summary>
    public DbSet<Plan> Plans { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of contributions.
    /// </summary>
    public DbSet<Contribution> Contributions { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of beneficiaries.
    /// </summary>
    public DbSet<Beneficiary> Beneficiaries { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of projections.
    /// </summary>
    public DbSet<Projection> Projections { get; set; } = null!;


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
    /// <summary>
    /// Configures the model that was discovered by convention from the entity types.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
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
            modelBuilder.Entity<Plan>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Contribution>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Beneficiary>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Projection>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CollegeSavingsPlannerContext).Assembly);
    }
}
