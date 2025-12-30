// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MedicationReminderSystem system.
/// </summary>
public class MedicationReminderSystemContext : DbContext, IMedicationReminderSystemContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="MedicationReminderSystemContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MedicationReminderSystemContext(DbContextOptions<MedicationReminderSystemContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Medication> Medications { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<DoseSchedule> DoseSchedules { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Refill> Refills { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Medication>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<DoseSchedule>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Refill>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicationReminderSystemContext).Assembly);
    }
}
