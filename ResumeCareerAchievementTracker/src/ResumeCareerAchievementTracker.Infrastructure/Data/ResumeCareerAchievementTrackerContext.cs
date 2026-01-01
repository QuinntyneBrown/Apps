// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ResumeCareerAchievementTracker.Core;
using Microsoft.EntityFrameworkCore;

using ResumeCareerAchievementTracker.Core.Model.UserAggregate;
using ResumeCareerAchievementTracker.Core.Model.UserAggregate.Entities;
namespace ResumeCareerAchievementTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the ResumeCareerAchievementTracker system.
/// </summary>
public class ResumeCareerAchievementTrackerContext : DbContext, IResumeCareerAchievementTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResumeCareerAchievementTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public ResumeCareerAchievementTrackerContext(DbContextOptions<ResumeCareerAchievementTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Achievement> Achievements { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Skill> Skills { get; set; } = null!;

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
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Achievement>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Skill>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Project>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ResumeCareerAchievementTrackerContext).Assembly);
    }
}
