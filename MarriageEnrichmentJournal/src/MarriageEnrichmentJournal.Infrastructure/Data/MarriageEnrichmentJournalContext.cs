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
    /// <summary>
    /// Initializes a new instance of the <see cref="MarriageEnrichmentJournalContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MarriageEnrichmentJournalContext(DbContextOptions<MarriageEnrichmentJournalContext> options)
        : base(options)
    {
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarriageEnrichmentJournalContext).Assembly);
    }
}
