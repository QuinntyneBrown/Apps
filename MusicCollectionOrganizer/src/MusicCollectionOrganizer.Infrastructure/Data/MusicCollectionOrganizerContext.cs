// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MusicCollectionOrganizer.Core;
using Microsoft.EntityFrameworkCore;

namespace MusicCollectionOrganizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MusicCollectionOrganizer system.
/// </summary>
public class MusicCollectionOrganizerContext : DbContext, IMusicCollectionOrganizerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="MusicCollectionOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MusicCollectionOrganizerContext(DbContextOptions<MusicCollectionOrganizerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Album> Albums { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Artist> Artists { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ListeningLog> ListeningLogs { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Album>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Artist>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ListeningLog>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MusicCollectionOrganizerContext).Assembly);
    }
}
