// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NutritionLabelScanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Comparison entity.
/// </summary>
public class ComparisonConfiguration : IEntityTypeConfiguration<Comparison>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Comparison> builder)
    {
        builder.ToTable("Comparisons");

        builder.HasKey(x => x.ComparisonId);

        builder.Property(x => x.ComparisonId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.ProductIds)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Results)
            .HasMaxLength(5000);

        builder.Property(x => x.WinnerProductId);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CreatedAt);
    }
}
