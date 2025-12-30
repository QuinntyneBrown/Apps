// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SkillDevelopmentTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace SkillDevelopmentTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SkillDevelopmentTracker system.
/// </summary>
public class SkillDevelopmentTrackerContext : DbContext, ISkillDevelopmentTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkillDevelopmentTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SkillDevelopmentTrackerContext(DbContextOptions<SkillDevelopmentTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Skill> Skills { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Course> Courses { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Certification> Certifications { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<LearningPath> LearningPaths { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Skill>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Course>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Certification>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<LearningPath>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SkillDevelopmentTrackerContext).Assembly);
    }
}
