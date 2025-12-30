// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VideoGameCollectionManager.Core;
using Microsoft.EntityFrameworkCore;

namespace VideoGameCollectionManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the VideoGameCollectionManager system.
/// </summary>
public class VideoGameCollectionManagerContext : DbContext, IVideoGameCollectionManagerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VideoGameCollectionManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public VideoGameCollectionManagerContext(DbContextOptions<VideoGameCollectionManagerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Game> Games { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PlaySession> PlaySessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Wishlist> Wishlists { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideoGameCollectionManagerContext).Assembly);
    }
}
