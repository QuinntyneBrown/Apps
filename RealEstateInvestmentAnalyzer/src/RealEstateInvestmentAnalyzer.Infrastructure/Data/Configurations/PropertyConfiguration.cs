// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateInvestmentAnalyzer.Core;

namespace RealEstateInvestmentAnalyzer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Property entity.
/// </summary>
public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Properties");

        builder.HasKey(x => x.PropertyId);

        builder.Property(x => x.PropertyId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.PropertyType)
            .IsRequired();

        builder.Property(x => x.PurchasePrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.PurchaseDate)
            .IsRequired();

        builder.Property(x => x.CurrentValue)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.SquareFeet)
            .IsRequired();

        builder.Property(x => x.Bedrooms)
            .IsRequired();

        builder.Property(x => x.Bathrooms)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.PropertyType);
        builder.HasIndex(x => x.PurchaseDate);
    }
}
