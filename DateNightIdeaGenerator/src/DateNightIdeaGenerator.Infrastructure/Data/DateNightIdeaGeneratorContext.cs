// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using Microsoft.EntityFrameworkCore;

namespace DateNightIdeaGenerator.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the DateNightIdeaGenerator system.
/// </summary>
public class DateNightIdeaGeneratorContext : DbContext, IDateNightIdeaGeneratorContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateNightIdeaGeneratorContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public DateNightIdeaGeneratorContext(DbContextOptions<DateNightIdeaGeneratorContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<DateIdea> DateIdeas { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Experience> Experiences { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Rating> Ratings { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DateNightIdeaGeneratorContext).Assembly);
    }
}
