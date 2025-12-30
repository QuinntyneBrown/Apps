// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MensGroupDiscussionTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MensGroupDiscussionTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MensGroupDiscussionTracker system.
/// </summary>
public class MensGroupDiscussionTrackerContext : DbContext, IMensGroupDiscussionTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MensGroupDiscussionTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MensGroupDiscussionTrackerContext(DbContextOptions<MensGroupDiscussionTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Group> Groups { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Meeting> Meetings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Topic> Topics { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Resource> Resources { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MensGroupDiscussionTrackerContext).Assembly);
    }
}
