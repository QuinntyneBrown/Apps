// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyPhotoAlbumOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the PersonTag entity.
/// </summary>
public class PersonTagConfiguration : IEntityTypeConfiguration<PersonTag>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<PersonTag> builder)
    {
        builder.ToTable("PersonTags");

        builder.HasKey(x => x.PersonTagId);

        builder.Property(x => x.PersonTagId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PhotoId)
            .IsRequired();

        builder.Property(x => x.PersonName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.CoordinateX);

        builder.Property(x => x.CoordinateY);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.PhotoId);
        builder.HasIndex(x => x.PersonName);
    }
}
