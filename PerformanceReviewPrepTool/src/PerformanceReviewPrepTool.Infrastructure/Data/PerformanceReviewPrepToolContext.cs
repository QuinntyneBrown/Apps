// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PerformanceReviewPrepTool.Core;
using Microsoft.EntityFrameworkCore;

namespace PerformanceReviewPrepTool.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PerformanceReviewPrepTool system.
/// </summary>
public class PerformanceReviewPrepToolContext : DbContext, IPerformanceReviewPrepToolContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceReviewPrepToolContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PerformanceReviewPrepToolContext(DbContextOptions<PerformanceReviewPrepToolContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<ReviewPeriod> ReviewPeriods { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Achievement> Achievements { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Feedback> Feedbacks { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<ReviewPeriod>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Achievement>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Goal>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Feedback>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PerformanceReviewPrepToolContext).Assembly);
    }
}
