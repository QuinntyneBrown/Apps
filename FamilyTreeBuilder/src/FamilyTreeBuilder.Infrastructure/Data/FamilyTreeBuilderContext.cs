// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using Microsoft.EntityFrameworkCore;

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

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
