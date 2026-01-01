// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using JobSearchOrganizer.Core;
using Microsoft.EntityFrameworkCore;

using JobSearchOrganizer.Core.Model.UserAggregate;
using JobSearchOrganizer.Core.Model.UserAggregate.Entities;
namespace JobSearchOrganizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the JobSearchOrganizer system.
/// </summary>
public class JobSearchOrganizerContext : DbContext, IJobSearchOrganizerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="JobSearchOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public JobSearchOrganizerContext(DbContextOptions<JobSearchOrganizerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Application> Applications { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Company> Companies { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Interview> Interviews { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Offer> Offers { get; set; } = null!;


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
            modelBuilder.Entity<Application>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Company>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Interview>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Offer>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobSearchOrganizerContext).Assembly);
    }
}
