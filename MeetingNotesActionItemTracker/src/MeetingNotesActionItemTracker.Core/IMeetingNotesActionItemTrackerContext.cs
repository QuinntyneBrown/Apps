// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Core;

/// <summary>
/// Represents the persistence surface for the MeetingNotesActionItemTracker system.
/// </summary>
public interface IMeetingNotesActionItemTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of meetings.
    /// </summary>
    DbSet<Meeting> Meetings { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of notes.
    /// </summary>
    DbSet<Note> Notes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of action items.
    /// </summary>
    DbSet<ActionItem> ActionItems { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
