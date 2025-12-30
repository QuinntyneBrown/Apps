// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MusicCollectionOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MusicCollectionOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Album entity.
/// </summary>
public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("Albums");

        builder.HasKey(x => x.AlbumId);

        builder.Property(x => x.AlbumId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.ArtistId);

        builder.Property(x => x.Format)
            .IsRequired();

        builder.Property(x => x.Genre)
            .IsRequired();

        builder.Property(x => x.ReleaseYear);

        builder.Property(x => x.Label)
            .HasMaxLength(200);

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.PurchaseDate);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Artist)
            .WithMany(x => x.Albums)
            .HasForeignKey(x => x.ArtistId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ArtistId);
        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => new { x.UserId, x.Genre });
    }
}
