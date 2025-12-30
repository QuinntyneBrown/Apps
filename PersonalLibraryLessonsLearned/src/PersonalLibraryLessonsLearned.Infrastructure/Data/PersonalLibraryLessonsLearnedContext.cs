// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLibraryLessonsLearned.Core;
using Microsoft.EntityFrameworkCore;

namespace PersonalLibraryLessonsLearned.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalLibraryLessonsLearned system.
/// </summary>
public class PersonalLibraryLessonsLearnedContext : DbContext, IPersonalLibraryLessonsLearnedContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalLibraryLessonsLearnedContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalLibraryLessonsLearnedContext(DbContextOptions<PersonalLibraryLessonsLearnedContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Lesson> Lessons { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Source> Sources { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<LessonReminder> Reminders { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalLibraryLessonsLearnedContext).Assembly);
    }
}
