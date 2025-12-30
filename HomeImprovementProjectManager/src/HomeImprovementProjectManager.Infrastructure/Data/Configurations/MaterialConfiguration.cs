// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeImprovementProjectManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Material entity.
/// </summary>
public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable("Materials");

        builder.HasKey(x => x.MaterialId);

        builder.Property(x => x.MaterialId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProjectId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.Unit)
            .HasMaxLength(50);

        builder.Property(x => x.UnitCost)
            .HasPrecision(18, 2);

        builder.Property(x => x.TotalCost)
            .HasPrecision(18, 2);

        builder.Property(x => x.Supplier)
            .HasMaxLength(200);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ProjectId);
        builder.HasIndex(x => x.Name);
    }
}
