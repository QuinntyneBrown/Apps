// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalNetWorthDashboard.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Asset entity.
/// </summary>
public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("Assets");

        builder.HasKey(x => x.AssetId);

        builder.Property(x => x.AssetId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.AssetType)
            .IsRequired();

        builder.Property(x => x.CurrentValue)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.Institution)
            .HasMaxLength(200);

        builder.Property(x => x.AccountNumber)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.LastUpdated)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(x => x.AssetType);
        builder.HasIndex(x => x.IsActive);
    }
}
