// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MarriageEnrichmentJournal.Core;
using Microsoft.EntityFrameworkCore;

namespace MarriageEnrichmentJournal.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MarriageEnrichmentJournal system.
/// </summary>
public class MarriageEnrichmentJournalContext : DbContext, IMarriageEnrichmentJournalContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="MarriageEnrichmentJournalContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MarriageEnrichmentJournalContext(DbContextOptions<MarriageEnrichmentJournalContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<JournalEntry> JournalEntries { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Gratitude> Gratitudes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Reflection> Reflections { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<JournalEntry>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Gratitude>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Reflection>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarriageEnrichmentJournalContext).Assembly);
    }
}
