// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuelEconomyTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the FillUp entity.
/// </summary>
public class FillUpConfiguration : IEntityTypeConfiguration<FillUp>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<FillUp> builder)
    {
        builder.ToTable("FillUps");

        builder.HasKey(x => x.FillUpId);

        builder.Property(x => x.FillUpId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VehicleId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Odometer)
            .IsRequired();

        builder.Property(x => x.GallonsFilled)
            .HasPrecision(8, 3)
            .IsRequired();

        builder.Property(x => x.PricePerGallon)
            .HasPrecision(6, 3);

        builder.Property(x => x.TotalCost)
            .HasPrecision(10, 2);

        builder.Property(x => x.IsFullTank)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.Location)
            .HasMaxLength(200);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Date);
        builder.HasIndex(x => new { x.VehicleId, x.Date });
    }
}
