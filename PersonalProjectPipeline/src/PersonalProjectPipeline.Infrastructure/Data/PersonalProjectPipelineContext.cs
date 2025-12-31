// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalProjectPipeline.Core;
using Microsoft.EntityFrameworkCore;

using PersonalProjectPipeline.Core.Model.UserAggregate;
using PersonalProjectPipeline.Core.Model.UserAggregate.Entities;
namespace PersonalProjectPipeline.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalProjectPipeline system.
/// </summary>
public class PersonalProjectPipelineContext : DbContext, IPersonalProjectPipelineContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalProjectPipelineContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalProjectPipelineContext(DbContextOptions<PersonalProjectPipelineContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ProjectTask> Tasks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Milestone> Milestones { get; set; } = null!;


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
            modelBuilder.Entity<Project>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ProjectTask>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Milestone>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalProjectPipelineContext).Assembly);
    }
}
