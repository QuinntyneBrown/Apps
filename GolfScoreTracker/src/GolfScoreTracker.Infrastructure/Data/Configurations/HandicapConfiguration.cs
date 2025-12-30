// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GolfScoreTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Handicap entity.
/// </summary>
public class HandicapConfiguration : IEntityTypeConfiguration<Handicap>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Handicap> builder)
    {
        builder.ToTable("Handicaps");

        builder.HasKey(x => x.HandicapId);

        builder.Property(x => x.HandicapId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.HandicapIndex)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(x => x.CalculatedDate)
            .IsRequired();

        builder.Property(x => x.RoundsUsed)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CalculatedDate);
    }
}
