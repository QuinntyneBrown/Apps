// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplianceWarrantyManualOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Appliance entity.
/// </summary>
public class ApplianceConfiguration : IEntityTypeConfiguration<Appliance>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Appliance> builder)
    {
        builder.ToTable("Appliances");

        builder.HasKey(x => x.ApplianceId);

        builder.Property(x => x.ApplianceId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.ApplianceType)
            .IsRequired();

        builder.Property(x => x.Brand)
            .HasMaxLength(100);

        builder.Property(x => x.ModelNumber)
            .HasMaxLength(100);

        builder.Property(x => x.SerialNumber)
            .HasMaxLength(100);

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Warranties)
            .WithOne(x => x.Appliance)
            .HasForeignKey(x => x.ApplianceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Manuals)
            .WithOne(x => x.Appliance)
            .HasForeignKey(x => x.ApplianceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ServiceRecords)
            .WithOne(x => x.Appliance)
            .HasForeignKey(x => x.ApplianceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ApplianceType);
        builder.HasIndex(x => new { x.UserId, x.ApplianceType });
    }
}
