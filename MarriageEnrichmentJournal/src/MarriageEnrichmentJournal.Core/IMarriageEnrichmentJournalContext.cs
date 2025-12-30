// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace MarriageEnrichmentJournal.Core;

/// <summary>
/// Represents the persistence surface for the MarriageEnrichmentJournal system.
/// </summary>
public interface IMarriageEnrichmentJournalContext
{
    /// <summary>
    /// Gets or sets the DbSet of journal entries.
    /// </summary>
    DbSet<JournalEntry> JournalEntries { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of gratitude entries.
    /// </summary>
    DbSet<Gratitude> Gratitudes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of reflections.
    /// </summary>
    DbSet<Reflection> Reflections { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
