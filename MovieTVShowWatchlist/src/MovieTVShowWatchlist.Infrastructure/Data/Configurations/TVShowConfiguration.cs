// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MovieTVShowWatchlist.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieTVShowWatchlist.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the TVShow entity.
/// </summary>
public class TVShowConfiguration : IEntityTypeConfiguration<TVShow>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TVShow> builder)
    {
        builder.ToTable("TVShows");

        builder.HasKey(x => x.TVShowId);

        builder.Property(x => x.TVShowId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.PremiereYear)
            .IsRequired();

        builder.Property(x => x.NumberOfSeasons);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.ExternalId)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.PremiereYear);
        builder.HasIndex(x => x.ExternalId);
    }
}
