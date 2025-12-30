// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GolfScoreTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the HoleScore entity.
/// </summary>
public class HoleScoreConfiguration : IEntityTypeConfiguration<HoleScore>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<HoleScore> builder)
    {
        builder.ToTable("HoleScores");

        builder.HasKey(x => x.HoleScoreId);

        builder.Property(x => x.HoleScoreId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.RoundId)
            .IsRequired();

        builder.Property(x => x.HoleNumber)
            .IsRequired();

        builder.Property(x => x.Par)
            .IsRequired();

        builder.Property(x => x.Score)
            .IsRequired();

        builder.Property(x => x.Putts);

        builder.Property(x => x.FairwayHit)
            .IsRequired();

        builder.Property(x => x.GreenInRegulation)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.RoundId);
        builder.HasIndex(x => x.HoleNumber);
    }
}
