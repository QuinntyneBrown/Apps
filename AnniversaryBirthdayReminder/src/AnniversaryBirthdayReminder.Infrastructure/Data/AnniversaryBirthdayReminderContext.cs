// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using AnniversaryBirthdayReminder.Core.Models.UserAggregate;
using AnniversaryBirthdayReminder.Core.Models.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnniversaryBirthdayReminder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the AnniversaryBirthdayReminder system.
/// </summary>
public class AnniversaryBirthdayReminderContext : DbContext, IAnniversaryBirthdayReminderContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnniversaryBirthdayReminderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public AnniversaryBirthdayReminderContext(DbContextOptions<AnniversaryBirthdayReminderContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<ImportantDate> ImportantDates { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Reminder> Reminders { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Gift> Gifts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Celebration> Celebrations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<User> Users { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Role> Roles { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<UserRole> UserRoles { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<ImportantDate>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Reminder>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Gift>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Celebration>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<User>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Role>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<UserRole>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnniversaryBirthdayReminderContext).Assembly);
    }
}
