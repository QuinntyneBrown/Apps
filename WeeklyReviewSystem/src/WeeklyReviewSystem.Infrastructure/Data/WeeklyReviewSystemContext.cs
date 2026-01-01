// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

using WeeklyReviewSystem.Core.Model.UserAggregate;
using WeeklyReviewSystem.Core.Model.UserAggregate.Entities;
namespace WeeklyReviewSystem.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the WeeklyReviewSystem system.
/// </summary>
public class WeeklyReviewSystemContext : DbContext, IWeeklyReviewSystemContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeeklyReviewSystemContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public WeeklyReviewSystemContext(DbContextOptions<WeeklyReviewSystemContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<WeeklyReview> Reviews { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Accomplishment> Accomplishments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Challenge> Challenges { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WeeklyPriority> Priorities { get; set; } = null!;


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
            modelBuilder.Entity<WeeklyReview>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Accomplishment>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Challenge>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<WeeklyPriority>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WeeklyReviewSystemContext).Assembly);
    }
}
