// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MovieTVShowWatchlist.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieTVShowWatchlist.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Episode entity.
/// </summary>
public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Episode> builder)
    {
        builder.ToTable("Episodes");

        builder.HasKey(x => x.EpisodeId);

        builder.Property(x => x.EpisodeId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TVShowId)
            .IsRequired();

        builder.Property(x => x.SeasonNumber)
            .IsRequired();

        builder.Property(x => x.EpisodeNumber)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(500);

        builder.Property(x => x.AirDate);

        builder.Property(x => x.Runtime);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasOne(x => x.TVShow)
            .WithMany(x => x.Episodes)
            .HasForeignKey(x => x.TVShowId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.TVShowId);
        builder.HasIndex(x => new { x.TVShowId, x.SeasonNumber, x.EpisodeNumber }).IsUnique();
    }
}
