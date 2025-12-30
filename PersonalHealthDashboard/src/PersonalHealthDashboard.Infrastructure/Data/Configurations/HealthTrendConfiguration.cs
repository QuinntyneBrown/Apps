// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalHealthDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalHealthDashboard.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the HealthTrend entity.
/// </summary>
public class HealthTrendConfiguration : IEntityTypeConfiguration<HealthTrend>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<HealthTrend> builder)
    {
        builder.ToTable("HealthTrends");

        builder.HasKey(x => x.HealthTrendId);

        builder.Property(x => x.HealthTrendId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.MetricName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.AverageValue)
            .IsRequired();

        builder.Property(x => x.MinValue)
            .IsRequired();

        builder.Property(x => x.MaxValue)
            .IsRequired();

        builder.Property(x => x.TrendDirection)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.PercentageChange)
            .IsRequired();

        builder.Property(x => x.Insights)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MetricName);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => x.EndDate);
    }
}
