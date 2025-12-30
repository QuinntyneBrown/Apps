// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MusicCollectionOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MusicCollectionOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Artist entity.
/// </summary>
public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.ToTable("Artists");

        builder.HasKey(x => x.ArtistId);

        builder.Property(x => x.ArtistId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Biography)
            .HasMaxLength(2000);

        builder.Property(x => x.Country)
            .HasMaxLength(100);

        builder.Property(x => x.FormedYear);

        builder.Property(x => x.Website)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => new { x.UserId, x.Name });
    }
}
