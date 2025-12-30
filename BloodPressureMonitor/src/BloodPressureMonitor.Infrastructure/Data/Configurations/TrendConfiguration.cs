// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodPressureMonitor.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Trend entity.
/// </summary>
public class TrendConfiguration : IEntityTypeConfiguration<Trend>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Trend> builder)
    {
        builder.ToTable("Trends");

        builder.HasKey(x => x.TrendId);

        builder.Property(x => x.TrendId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.AverageSystolic)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.AverageDiastolic)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.HighestSystolic)
            .IsRequired();

        builder.Property(x => x.HighestDiastolic)
            .IsRequired();

        builder.Property(x => x.LowestSystolic)
            .IsRequired();

        builder.Property(x => x.LowestDiastolic)
            .IsRequired();

        builder.Property(x => x.ReadingCount)
            .IsRequired();

        builder.Property(x => x.TrendDirection)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Insights)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Ignore(x => x.IsImproving());
        builder.Ignore(x => x.GetPeriodDuration());

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => x.EndDate);
        builder.HasIndex(x => new { x.UserId, x.StartDate, x.EndDate });
    }
}
