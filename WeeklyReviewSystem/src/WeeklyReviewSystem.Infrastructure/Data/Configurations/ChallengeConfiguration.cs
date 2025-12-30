// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Challenge entity.
/// </summary>
public class ChallengeConfiguration : IEntityTypeConfiguration<Challenge>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Challenge> builder)
    {
        builder.ToTable("Challenges");

        builder.HasKey(x => x.ChallengeId);

        builder.Property(x => x.ChallengeId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.WeeklyReviewId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Resolution)
            .HasMaxLength(1000);

        builder.Property(x => x.IsResolved)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.LessonsLearned)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.WeeklyReviewId);
        builder.HasIndex(x => x.IsResolved);
    }
}
