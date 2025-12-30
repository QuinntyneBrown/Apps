// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using Microsoft.EntityFrameworkCore;

namespace LetterToFutureSelf.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the LetterToFutureSelf system.
/// </summary>
public class LetterToFutureSelfContext : DbContext, ILetterToFutureSelfContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LetterToFutureSelfContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public LetterToFutureSelfContext(DbContextOptions<LetterToFutureSelfContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Letter> Letters { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<DeliverySchedule> DeliverySchedules { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LetterToFutureSelfContext).Assembly);
    }
}
