// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using Microsoft.EntityFrameworkCore;

using FamilyTreeBuilder.Core.Model.UserAggregate;
using FamilyTreeBuilder.Core.Model.UserAggregate.Entities;
namespace FamilyTreeBuilder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FamilyTreeBuilder system.
/// </summary>
public class FamilyTreeBuilderContext : DbContext, IFamilyTreeBuilderContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="FamilyTreeBuilderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FamilyTreeBuilderContext(DbContextOptions<FamilyTreeBuilderContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Person> Persons { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Relationship> Relationships { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Story> Stories { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<FamilyPhoto> FamilyPhotos { get; set; } = null!;


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
            modelBuilder.Entity<Person>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Relationship>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Story>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<FamilyPhoto>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FamilyTreeBuilderContext).Assembly);
    }
}
