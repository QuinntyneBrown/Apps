// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeInventoryManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeInventoryManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ValueEstimate entity.
/// </summary>
public class ValueEstimateConfiguration : IEntityTypeConfiguration<ValueEstimate>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ValueEstimate> builder)
    {
        builder.ToTable("ValueEstimates");

        builder.HasKey(x => x.ValueEstimateId);

        builder.Property(x => x.ValueEstimateId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ItemId)
            .IsRequired();

        builder.Property(x => x.EstimatedValue)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.EstimationDate)
            .IsRequired();

        builder.Property(x => x.Source)
            .HasMaxLength(200);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ItemId);
        builder.HasIndex(x => x.EstimationDate);
    }
}
