// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FriendGroupEventCoordinator system.
/// </summary>
public class FriendGroupEventCoordinatorContext : DbContext, IFriendGroupEventCoordinatorContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FriendGroupEventCoordinatorContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FriendGroupEventCoordinatorContext(DbContextOptions<FriendGroupEventCoordinatorContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Event> Events { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<RSVP> RSVPs { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Group> Groups { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Member> Members { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FriendGroupEventCoordinatorContext).Assembly);
    }
}
