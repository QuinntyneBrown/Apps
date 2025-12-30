// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalMissionStatementBuilder.Core;
using Microsoft.EntityFrameworkCore;

namespace PersonalMissionStatementBuilder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalMissionStatementBuilder system.
/// </summary>
public class PersonalMissionStatementBuilderContext : DbContext, IPersonalMissionStatementBuilderContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalMissionStatementBuilderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalMissionStatementBuilderContext(DbContextOptions<PersonalMissionStatementBuilderContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<MissionStatement> MissionStatements { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Value> Values { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Progress> Progresses { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<MissionStatement>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Value>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Goal>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Progress>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalMissionStatementBuilderContext).Assembly);
    }
}
