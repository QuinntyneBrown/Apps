// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TimeAuditTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace TimeAuditTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the TimeAuditTracker system.
/// </summary>
public class TimeAuditTrackerContext : DbContext, ITimeAuditTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeAuditTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public TimeAuditTrackerContext(DbContextOptions<TimeAuditTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<TimeBlock> TimeBlocks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<AuditReport> AuditReports { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<TimeBlock>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<AuditReport>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Goal>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TimeAuditTrackerContext).Assembly);
    }
}
