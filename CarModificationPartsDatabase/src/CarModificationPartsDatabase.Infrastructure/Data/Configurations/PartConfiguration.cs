// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarModificationPartsDatabase.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Part entity.
/// </summary>
public class PartConfiguration : IEntityTypeConfiguration<Part>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Part> builder)
    {
        builder.ToTable("Parts");

        builder.HasKey(x => x.PartId);

        builder.Property(x => x.PartId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.PartNumber)
            .HasMaxLength(100);

        builder.Property(x => x.Manufacturer)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.CompatibleVehicles)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.WarrantyInfo)
            .HasMaxLength(1000);

        builder.Property(x => x.Weight)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Dimensions)
            .HasMaxLength(200);

        builder.Property(x => x.InStock)
            .IsRequired();

        builder.Property(x => x.Supplier)
            .HasMaxLength(300);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.PartNumber);
        builder.HasIndex(x => x.Manufacturer);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.InStock);
    }
}
