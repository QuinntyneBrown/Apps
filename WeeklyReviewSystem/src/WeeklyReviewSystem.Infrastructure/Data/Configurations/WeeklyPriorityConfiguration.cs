// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the WeeklyPriority entity.
/// </summary>
public class WeeklyPriorityConfiguration : IEntityTypeConfiguration<WeeklyPriority>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WeeklyPriority> builder)
    {
        builder.ToTable("WeeklyPriorities");

        builder.HasKey(x => x.WeeklyPriorityId);

        builder.Property(x => x.WeeklyPriorityId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.WeeklyReviewId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Level)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasMaxLength(100);

        builder.Property(x => x.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.WeeklyReviewId);
        builder.HasIndex(x => x.Level);
        builder.HasIndex(x => new { x.WeeklyReviewId, x.IsCompleted });
    }
}
