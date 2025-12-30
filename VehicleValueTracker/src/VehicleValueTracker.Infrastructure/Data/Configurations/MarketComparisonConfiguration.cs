// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleValueTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VehicleValueTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the MarketComparison entity.
/// </summary>
public class MarketComparisonConfiguration : IEntityTypeConfiguration<MarketComparison>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<MarketComparison> builder)
    {
        builder.ToTable("MarketComparisons");

        builder.HasKey(x => x.MarketComparisonId);

        builder.Property(x => x.MarketComparisonId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VehicleId)
            .IsRequired();

        builder.Property(x => x.ComparisonDate)
            .IsRequired();

        builder.Property(x => x.ListingSource)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.ComparableYear)
            .IsRequired();

        builder.Property(x => x.ComparableMake)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ComparableModel)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ComparableTrim)
            .HasMaxLength(100);

        builder.Property(x => x.ComparableMileage)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.AskingPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(200);

        builder.Property(x => x.Condition)
            .HasMaxLength(100);

        builder.Property(x => x.ListingUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.ComparisonDate);
        builder.HasIndex(x => new { x.VehicleId, x.IsActive });
    }
}
