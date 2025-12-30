// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarModificationPartsDatabase.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Installation entity.
/// </summary>
public class InstallationConfiguration : IEntityTypeConfiguration<Installation>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Installation> builder)
    {
        builder.ToTable("Installations");

        builder.HasKey(x => x.InstallationId);

        builder.Property(x => x.InstallationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ModificationId)
            .IsRequired();

        builder.Property(x => x.VehicleInfo)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.InstallationDate)
            .IsRequired();

        builder.Property(x => x.InstalledBy)
            .HasMaxLength(300);

        builder.Property(x => x.InstallationCost)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.PartsCost)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.LaborHours)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.PartsUsed)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.DifficultyRating);

        builder.Property(x => x.SatisfactionRating);

        builder.Property(x => x.Photos)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.IsCompleted)
            .IsRequired();

        builder.HasIndex(x => x.ModificationId);
        builder.HasIndex(x => x.InstallationDate);
        builder.HasIndex(x => x.IsCompleted);
    }
}
