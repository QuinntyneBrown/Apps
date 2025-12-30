// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BucketListManager.Core;
using Microsoft.EntityFrameworkCore;

namespace BucketListManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the BucketListManager system.
/// </summary>
public class BucketListManagerContext : DbContext, IBucketListManagerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BucketListManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public BucketListManagerContext(DbContextOptions<BucketListManagerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<BucketListItem> BucketListItems { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Milestone> Milestones { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Memory> Memories { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BucketListManagerContext).Assembly);
    }
}
