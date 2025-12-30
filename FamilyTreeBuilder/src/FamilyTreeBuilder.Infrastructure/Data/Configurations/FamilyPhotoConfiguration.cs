// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTreeBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the FamilyPhoto entity.
/// </summary>
public class FamilyPhotoConfiguration : IEntityTypeConfiguration<FamilyPhoto>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<FamilyPhoto> builder)
    {
        builder.ToTable("FamilyPhotos");

        builder.HasKey(x => x.FamilyPhotoId);

        builder.Property(x => x.FamilyPhotoId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PersonId)
            .IsRequired();

        builder.Property(x => x.PhotoUrl)
            .HasMaxLength(2000);

        builder.Property(x => x.Caption)
            .HasMaxLength(1000);

        builder.Property(x => x.DateTaken);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.PersonId);
        builder.HasIndex(x => x.DateTaken);
    }
}
