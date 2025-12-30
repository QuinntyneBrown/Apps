// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuelEconomyTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the EfficiencyReport entity.
/// </summary>
public class EfficiencyReportConfiguration : IEntityTypeConfiguration<EfficiencyReport>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<EfficiencyReport> builder)
    {
        builder.ToTable("EfficiencyReports");

        builder.HasKey(x => x.EfficiencyReportId);

        builder.Property(x => x.EfficiencyReportId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VehicleId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.PeriodStartDate)
            .IsRequired();

        builder.Property(x => x.PeriodEndDate)
            .IsRequired();

        builder.Property(x => x.TotalMilesDriven)
            .IsRequired();

        builder.Property(x => x.TotalGallonsUsed)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.AverageMPG)
            .HasPrecision(6, 2)
            .IsRequired();

        builder.Property(x => x.TotalCost)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.AverageCostPerMile)
            .HasPrecision(6, 3)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.VehicleId, x.PeriodStartDate, x.PeriodEndDate });
    }
}
