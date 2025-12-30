// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotographySessionLogger.Core;

namespace PhotographySessionLogger.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Gear entity.
/// </summary>
public class GearConfiguration : IEntityTypeConfiguration<Gear>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Gear> builder)
    {
        builder.ToTable("Gears");

        builder.HasKey(x => x.GearId);

        builder.Property(x => x.GearId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.GearType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Brand)
            .HasMaxLength(100);

        builder.Property(x => x.Model)
            .HasMaxLength(200);

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.GearType);
    }
}
