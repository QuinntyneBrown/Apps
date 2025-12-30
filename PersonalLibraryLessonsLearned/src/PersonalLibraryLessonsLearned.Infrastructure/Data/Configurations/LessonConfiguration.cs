// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLibraryLessonsLearned.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalLibraryLessonsLearned.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Lesson entity.
/// </summary>
public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lessons");

        builder.HasKey(x => x.LessonId);

        builder.Property(x => x.LessonId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.SourceId);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.Tags)
            .HasMaxLength(500);

        builder.Property(x => x.DateLearned)
            .IsRequired();

        builder.Property(x => x.Application)
            .HasMaxLength(2000);

        builder.Property(x => x.IsApplied)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.SourceId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.DateLearned);
        builder.HasIndex(x => x.IsApplied);
    }
}
