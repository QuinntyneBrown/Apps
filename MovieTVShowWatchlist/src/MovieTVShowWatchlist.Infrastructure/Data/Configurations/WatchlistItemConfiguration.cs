// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MovieTVShowWatchlist.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieTVShowWatchlist.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the WatchlistItem entity.
/// </summary>
public class WatchlistItemConfiguration : IEntityTypeConfiguration<WatchlistItem>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WatchlistItem> builder)
    {
        builder.ToTable("WatchlistItems");

        builder.HasKey(x => x.WatchlistItemId);

        builder.Property(x => x.WatchlistItemId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ContentId)
            .IsRequired();

        builder.Property(x => x.ContentType)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.AddedDate)
            .IsRequired();

        builder.Property(x => x.PriorityLevel);

        builder.Property(x => x.PriorityRank);

        builder.Property(x => x.RecommendationSource)
            .HasMaxLength(200);

        builder.Property(x => x.MoodCategory)
            .HasMaxLength(100);

        builder.Property(x => x.WatchOrderPreference);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.DeletedAt);

        builder.Property(x => x.RemovalReason);

        builder.HasOne(x => x.User)
            .WithMany(x => x.WatchlistItems)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ContentId);
        builder.HasIndex(x => new { x.UserId, x.ContentId, x.ContentType });
    }
}
