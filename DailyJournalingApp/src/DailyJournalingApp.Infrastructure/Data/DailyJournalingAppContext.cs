// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DailyJournalingApp.Core;
using Microsoft.EntityFrameworkCore;

namespace DailyJournalingApp.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the DailyJournalingApp system.
/// </summary>
public class DailyJournalingAppContext : DbContext, IDailyJournalingAppContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DailyJournalingAppContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public DailyJournalingAppContext(DbContextOptions<DailyJournalingAppContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<JournalEntry> JournalEntries { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Prompt> Prompts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Tag> Tags { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DailyJournalingAppContext).Assembly);
    }
}
