// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Infrastructure.Data;

/// <summary>
/// Represents the Entity Framework database context for the ConferenceEventManager system.
/// </summary>
public class ConferenceEventManagerContext : DbContext, IConferenceEventManagerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConferenceEventManagerContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public ConferenceEventManagerContext(DbContextOptions<ConferenceEventManagerContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the DbSet of events.
    /// </summary>
    public DbSet<Event> Events { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of sessions.
    /// </summary>
    public DbSet<Session> Sessions { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of contacts.
    /// </summary>
    public DbSet<Contact> Contacts { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of notes.
    /// </summary>
    public DbSet<Note> Notes { get; set; } = null!;

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConferenceEventManagerContext).Assembly);
    }
}
