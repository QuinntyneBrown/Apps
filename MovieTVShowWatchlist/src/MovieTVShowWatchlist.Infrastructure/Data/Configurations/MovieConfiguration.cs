// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MovieTVShowWatchlist.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieTVShowWatchlist.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Movie entity.
/// </summary>
public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(x => x.MovieId);

        builder.Property(x => x.MovieId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.ReleaseYear)
            .IsRequired();

        builder.Property(x => x.Director)
            .HasMaxLength(200);

        builder.Property(x => x.Runtime);

        builder.Property(x => x.ExternalId)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.ReleaseYear);
        builder.HasIndex(x => x.ExternalId);
    }
}
