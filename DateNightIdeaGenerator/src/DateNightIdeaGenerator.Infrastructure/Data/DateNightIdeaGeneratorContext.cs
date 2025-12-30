// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using Microsoft.EntityFrameworkCore;

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

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
