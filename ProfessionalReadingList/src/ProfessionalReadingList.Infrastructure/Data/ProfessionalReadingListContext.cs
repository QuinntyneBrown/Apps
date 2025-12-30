// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using ProfessionalReadingList.Core;

namespace ProfessionalReadingList.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the ProfessionalReadingList system.
/// </summary>
public class ProfessionalReadingListContext : DbContext, IProfessionalReadingListContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProfessionalReadingListContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public ProfessionalReadingListContext(DbContextOptions<ProfessionalReadingListContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Resource> Resources { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ReadingProgress> ReadingProgress { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Note> Notes { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfessionalReadingListContext).Assembly);
    }
}
