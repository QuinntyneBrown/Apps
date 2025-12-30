// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLibraryLessonsLearned.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalLibraryLessonsLearned.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the LessonReminder entity.
/// </summary>
public class LessonReminderConfiguration : IEntityTypeConfiguration<LessonReminder>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<LessonReminder> builder)
    {
        builder.ToTable("LessonReminders");

        builder.HasKey(x => x.LessonReminderId);

        builder.Property(x => x.LessonReminderId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.LessonId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ReminderDateTime)
            .IsRequired();

        builder.Property(x => x.Message)
            .HasMaxLength(500);

        builder.Property(x => x.IsSent)
            .IsRequired();

        builder.Property(x => x.IsDismissed)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.LessonId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ReminderDateTime);
        builder.HasIndex(x => x.IsSent);
    }
}
