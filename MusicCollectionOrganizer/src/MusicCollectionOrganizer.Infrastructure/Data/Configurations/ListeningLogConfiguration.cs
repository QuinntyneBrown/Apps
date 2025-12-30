// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MusicCollectionOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MusicCollectionOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ListeningLog entity.
/// </summary>
public class ListeningLogConfiguration : IEntityTypeConfiguration<ListeningLog>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ListeningLog> builder)
    {
        builder.ToTable("ListeningLogs");

        builder.HasKey(x => x.ListeningLogId);

        builder.Property(x => x.ListeningLogId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.AlbumId)
            .IsRequired();

        builder.Property(x => x.ListeningDate)
            .IsRequired();

        builder.Property(x => x.Rating);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Album)
            .WithMany(x => x.ListeningLogs)
            .HasForeignKey(x => x.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.AlbumId);
        builder.HasIndex(x => x.ListeningDate);
        builder.HasIndex(x => new { x.UserId, x.ListeningDate });
    }
}
