// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MeetingNotesActionItemTracker system.
/// </summary>
public class MeetingNotesActionItemTrackerContext : DbContext, IMeetingNotesActionItemTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeetingNotesActionItemTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MeetingNotesActionItemTrackerContext(DbContextOptions<MeetingNotesActionItemTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Meeting> Meetings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Note> Notes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ActionItem> ActionItems { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeetingNotesActionItemTrackerContext).Assembly);
    }
}
