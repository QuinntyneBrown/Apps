// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NutritionLabelScanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the NutritionInfo entity.
/// </summary>
public class NutritionInfoConfiguration : IEntityTypeConfiguration<NutritionInfo>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<NutritionInfo> builder)
    {
        builder.ToTable("NutritionInfos");

        builder.HasKey(x => x.NutritionInfoId);

        builder.Property(x => x.NutritionInfoId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Calories)
            .IsRequired();

        builder.Property(x => x.TotalFat)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.SaturatedFat)
            .HasPrecision(10, 2);

        builder.Property(x => x.TransFat)
            .HasPrecision(10, 2);

        builder.Property(x => x.Cholesterol)
            .HasPrecision(10, 2);

        builder.Property(x => x.Sodium)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.TotalCarbohydrates)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.DietaryFiber)
            .HasPrecision(10, 2);

        builder.Property(x => x.TotalSugars)
            .HasPrecision(10, 2);

        builder.Property(x => x.Protein)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.AdditionalNutrients)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ProductId).IsUnique();
        builder.HasIndex(x => x.Calories);
    }
}
