// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VideoGameCollectionManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VideoGameCollectionManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the PlaySession entity.
/// </summary>
public class PlaySessionConfiguration : IEntityTypeConfiguration<PlaySession>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<PlaySession> builder)
    {
        builder.ToTable("PlaySessions");

        builder.HasKey(x => x.PlaySessionId);

        builder.Property(x => x.PlaySessionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.GameId)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.GameId);
        builder.HasIndex(x => x.StartTime);
        builder.HasIndex(x => new { x.UserId, x.GameId });
    }
}
