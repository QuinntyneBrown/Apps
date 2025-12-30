// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyPhotoAlbumOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Photo entity.
/// </summary>
public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.ToTable("Photos");

        builder.HasKey(x => x.PhotoId);

        builder.Property(x => x.PhotoId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.AlbumId);

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.FileUrl)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.ThumbnailUrl)
            .HasMaxLength(2000);

        builder.Property(x => x.Caption)
            .HasMaxLength(1000);

        builder.Property(x => x.DateTaken);

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.IsFavorite)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Tags)
            .WithMany(x => x.Photos)
            .UsingEntity(j => j.ToTable("PhotoTags"));

        builder.HasMany(x => x.PersonTags)
            .WithOne(x => x.Photo)
            .HasForeignKey(x => x.PhotoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.AlbumId);
        builder.HasIndex(x => x.DateTaken);
        builder.HasIndex(x => x.IsFavorite);
    }
}
