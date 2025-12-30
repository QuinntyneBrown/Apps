// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotographySessionLogger.Core;

namespace PhotographySessionLogger.Infrastructure;

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

        builder.Property(x => x.SessionId)
            .IsRequired();

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.FilePath)
            .HasMaxLength(1000);

        builder.Property(x => x.CameraSettings)
            .HasMaxLength(500);

        builder.Property(x => x.Tags)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.SessionId);
        builder.HasIndex(x => x.Rating);

        builder.HasOne(x => x.Session)
            .WithMany(x => x.Photos)
            .HasForeignKey(x => x.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
