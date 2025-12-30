// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarModificationPartsDatabase.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Modification entity.
/// </summary>
public class ModificationConfiguration : IEntityTypeConfiguration<Modification>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Modification> builder)
    {
        builder.ToTable("Modifications");

        builder.HasKey(x => x.ModificationId);

        builder.Property(x => x.ModificationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Manufacturer)
            .HasMaxLength(300);

        builder.Property(x => x.EstimatedCost)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.DifficultyLevel);

        builder.Property(x => x.EstimatedInstallationTime)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.PerformanceGain)
            .HasMaxLength(1000);

        builder.Property(x => x.CompatibleVehicles)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.RequiredTools)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.Manufacturer);
    }
}
