// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Core;

/// <summary>
/// Represents the persistence surface for the ConferenceEventManager system.
/// </summary>
public interface IConferenceEventManagerContext
{
    /// <summary>
    /// Gets or sets the DbSet of events.
    /// </summary>
    DbSet<Event> Events { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of sessions.
    /// </summary>
    DbSet<Session> Sessions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of contacts.
    /// </summary>
    DbSet<Contact> Contacts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of notes.
    /// </summary>
    DbSet<Note> Notes { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
