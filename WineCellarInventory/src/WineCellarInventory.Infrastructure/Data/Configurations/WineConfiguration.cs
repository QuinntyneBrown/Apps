// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WineCellarInventory.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WineCellarInventory.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Wine entity.
/// </summary>
public class WineConfiguration : IEntityTypeConfiguration<Wine>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Wine> builder)
    {
        builder.ToTable("Wines");

        builder.HasKey(x => x.WineId);

        builder.Property(x => x.WineId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.WineType)
            .IsRequired();

        builder.Property(x => x.Region)
            .IsRequired();

        builder.Property(x => x.Vintage);

        builder.Property(x => x.Producer)
            .HasMaxLength(200);

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.BottleCount)
            .IsRequired();

        builder.Property(x => x.StorageLocation)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.WineType);
        builder.HasIndex(x => x.Region);
        builder.HasIndex(x => x.Vintage);
    }
}
