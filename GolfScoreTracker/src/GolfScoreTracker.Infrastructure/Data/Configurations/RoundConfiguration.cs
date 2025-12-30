// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GolfScoreTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Round entity.
/// </summary>
public class RoundConfiguration : IEntityTypeConfiguration<Round>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Round> builder)
    {
        builder.ToTable("Rounds");

        builder.HasKey(x => x.RoundId);

        builder.Property(x => x.RoundId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CourseId)
            .IsRequired();

        builder.Property(x => x.PlayedDate)
            .IsRequired();

        builder.Property(x => x.TotalScore)
            .IsRequired();

        builder.Property(x => x.TotalPar)
            .IsRequired();

        builder.Property(x => x.Weather)
            .HasMaxLength(200);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CourseId);
        builder.HasIndex(x => x.PlayedDate);
    }
}
