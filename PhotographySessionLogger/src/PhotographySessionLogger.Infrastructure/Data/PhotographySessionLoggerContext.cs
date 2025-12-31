// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using PhotographySessionLogger.Core;

using PhotographySessionLogger.Core.Model.UserAggregate;
using PhotographySessionLogger.Core.Model.UserAggregate.Entities;
namespace PhotographySessionLogger.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PhotographySessionLogger system.
/// </summary>
public class PhotographySessionLoggerContext : DbContext, IPhotographySessionLoggerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PhotographySessionLoggerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PhotographySessionLoggerContext(DbContextOptions<PhotographySessionLoggerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Session> Sessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Photo> Photos { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Gear> Gears { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Project> Projects { get; set; } = null!;


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
            modelBuilder.Entity<Session>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Photo>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Gear>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Project>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhotographySessionLoggerContext).Assembly);
    }
}
