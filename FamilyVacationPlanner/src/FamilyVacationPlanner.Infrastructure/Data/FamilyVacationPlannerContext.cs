// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyVacationPlanner.Core;
using Microsoft.EntityFrameworkCore;

namespace FamilyVacationPlanner.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FamilyVacationPlanner system.
/// </summary>
public class FamilyVacationPlannerContext : DbContext, IFamilyVacationPlannerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="FamilyVacationPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FamilyVacationPlannerContext(DbContextOptions<FamilyVacationPlannerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Trip> Trips { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Itinerary> Itineraries { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Booking> Bookings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<VacationBudget> VacationBudgets { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PackingList> PackingLists { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Trip>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Itinerary>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Booking>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<VacationBudget>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<PackingList>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FamilyVacationPlannerContext).Assembly);
    }
}
