// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Core;
using Microsoft.EntityFrameworkCore;

using AnnualHealthScreeningReminder.Core.Model.UserAggregate;
using AnnualHealthScreeningReminder.Core.Model.UserAggregate.Entities;
namespace AnnualHealthScreeningReminder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the AnnualHealthScreeningReminder system.
/// </summary>
public class AnnualHealthScreeningReminderContext : DbContext, IAnnualHealthScreeningReminderContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnnualHealthScreeningReminderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public AnnualHealthScreeningReminderContext(DbContextOptions<AnnualHealthScreeningReminderContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Screening> Screenings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Appointment> Appointments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Reminder> Reminders { get; set; } = null!;


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
            modelBuilder.Entity<Screening>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Appointment>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Reminder>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnnualHealthScreeningReminderContext).Assembly);
    }
}
