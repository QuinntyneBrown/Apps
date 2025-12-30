// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLibraryLessonsLearned.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalLibraryLessonsLearned.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Source entity.
/// </summary>
public class SourceConfiguration : IEntityTypeConfiguration<Source>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.ToTable("Sources");

        builder.HasKey(x => x.SourceId);

        builder.Property(x => x.SourceId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Author)
            .HasMaxLength(200);

        builder.Property(x => x.SourceType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Url)
            .HasMaxLength(500);

        builder.Property(x => x.DateConsumed);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.SourceType);
        builder.HasIndex(x => x.DateConsumed);
    }
}
