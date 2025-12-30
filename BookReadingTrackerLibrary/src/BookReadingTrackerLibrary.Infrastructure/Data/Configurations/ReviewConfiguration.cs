// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BookReadingTrackerLibrary.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReadingTrackerLibrary.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Review entity.
/// </summary>
public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(x => x.ReviewId);

        builder.Property(x => x.ReviewId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.BookId)
            .IsRequired();

        builder.Property(x => x.Rating)
            .IsRequired();

        builder.Property(x => x.ReviewText)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.IsRecommended)
            .IsRequired();

        builder.Property(x => x.ReviewDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.BookId);
        builder.HasIndex(x => x.Rating);
        builder.HasIndex(x => x.ReviewDate);
    }
}
