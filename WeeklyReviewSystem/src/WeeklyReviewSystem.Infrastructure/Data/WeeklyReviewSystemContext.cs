// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the WeeklyReviewSystem system.
/// </summary>
public class WeeklyReviewSystemContext : DbContext, IWeeklyReviewSystemContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WeeklyReviewSystemContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public WeeklyReviewSystemContext(DbContextOptions<WeeklyReviewSystemContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<WeeklyReview> Reviews { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Accomplishment> Accomplishments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Challenge> Challenges { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WeeklyPriority> Priorities { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WeeklyReviewSystemContext).Assembly);
    }
}
