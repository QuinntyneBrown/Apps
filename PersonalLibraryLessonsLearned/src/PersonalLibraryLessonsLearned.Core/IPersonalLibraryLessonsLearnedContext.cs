// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace PersonalLibraryLessonsLearned.Core;

/// <summary>
/// Represents the persistence surface for the PersonalLibraryLessonsLearned system.
/// </summary>
public interface IPersonalLibraryLessonsLearnedContext
{
    /// <summary>
    /// Gets or sets the DbSet of lessons.
    /// </summary>
    DbSet<Lesson> Lessons { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of sources.
    /// </summary>
    DbSet<Source> Sources { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of reminders.
    /// </summary>
    DbSet<LessonReminder> Reminders { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
