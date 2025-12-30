// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NeighborhoodSocialNetwork.Core;
using Microsoft.EntityFrameworkCore;

namespace NeighborhoodSocialNetwork.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the NeighborhoodSocialNetwork system.
/// </summary>
public class NeighborhoodSocialNetworkContext : DbContext, INeighborhoodSocialNetworkContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="NeighborhoodSocialNetworkContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public NeighborhoodSocialNetworkContext(DbContextOptions<NeighborhoodSocialNetworkContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Neighbor> Neighbors { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Event> Events { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Recommendation> Recommendations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Message> Messages { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Neighbor>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Event>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Recommendation>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Message>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NeighborhoodSocialNetworkContext).Assembly);
    }
}
