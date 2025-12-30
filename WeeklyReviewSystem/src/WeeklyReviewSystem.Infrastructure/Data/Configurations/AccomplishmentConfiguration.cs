// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeeklyReviewSystem.Core;

namespace WeeklyReviewSystem.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Accomplishment entity.
/// </summary>
public class AccomplishmentConfiguration : IEntityTypeConfiguration<Accomplishment>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Accomplishment> builder)
    {
        builder.ToTable("Accomplishments");

        builder.HasKey(x => x.AccomplishmentId);

        builder.Property(x => x.AccomplishmentId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.WeeklyReviewId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Category)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.WeeklyReviewId);
        builder.HasIndex(x => x.Category);
    }
}
