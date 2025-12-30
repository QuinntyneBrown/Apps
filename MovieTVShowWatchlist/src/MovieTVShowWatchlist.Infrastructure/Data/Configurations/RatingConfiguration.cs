// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MovieTVShowWatchlist.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieTVShowWatchlist.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Rating entity.
/// </summary>
public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("Ratings");

        builder.HasKey(x => x.RatingId);

        builder.Property(x => x.RatingId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ContentId)
            .IsRequired();

        builder.Property(x => x.ContentType)
            .IsRequired();

        builder.Property(x => x.RatingValue)
            .HasPrecision(3, 1)
            .IsRequired();

        builder.Property(x => x.RatingScale)
            .IsRequired();

        builder.Property(x => x.RatingDate)
            .IsRequired();

        builder.Property(x => x.ViewingDate);

        builder.Property(x => x.IsRewatchRating)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Mood)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Ratings)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ContentId);
        builder.HasIndex(x => new { x.UserId, x.ContentId, x.ContentType });
    }
}
