// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MorningRoutineBuilder.Core;
using Microsoft.EntityFrameworkCore;

using MorningRoutineBuilder.Core.Models.UserAggregate;
using MorningRoutineBuilder.Core.Models.UserAggregate.Entities;
namespace MorningRoutineBuilder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MorningRoutineBuilder system.
/// </summary>
public class MorningRoutineBuilderContext : DbContext, IMorningRoutineBuilderContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="MorningRoutineBuilderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MorningRoutineBuilderContext(DbContextOptions<MorningRoutineBuilderContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Routine> Routines { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<RoutineTask> Tasks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<CompletionLog> CompletionLogs { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Streak> Streaks { get; set; } = null!;


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
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Routine>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<RoutineTask>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<CompletionLog>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Streak>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MorningRoutineBuilderContext).Assembly);
    }
}
