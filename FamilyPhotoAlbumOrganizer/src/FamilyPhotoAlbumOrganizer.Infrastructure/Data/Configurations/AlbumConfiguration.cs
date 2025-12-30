// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyPhotoAlbumOrganizer.Infrastructure;

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

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.CoverPhotoUrl)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Photos)
            .WithOne(x => x.Album)
            .HasForeignKey(x => x.AlbumId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CreatedDate);
    }
}
