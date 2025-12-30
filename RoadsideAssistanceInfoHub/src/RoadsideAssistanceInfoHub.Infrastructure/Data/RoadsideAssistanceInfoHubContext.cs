// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RoadsideAssistanceInfoHub.Core;
using Microsoft.EntityFrameworkCore;

namespace RoadsideAssistanceInfoHub.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the RoadsideAssistanceInfoHub system.
/// </summary>
public class RoadsideAssistanceInfoHubContext : DbContext, IRoadsideAssistanceInfoHubContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoadsideAssistanceInfoHubContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public RoadsideAssistanceInfoHubContext(DbContextOptions<RoadsideAssistanceInfoHubContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<InsuranceInfo> InsuranceInfos { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<EmergencyContact> EmergencyContacts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Policy> Policies { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Vehicle>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<InsuranceInfo>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<EmergencyContact>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Policy>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoadsideAssistanceInfoHubContext).Assembly);
    }
}
