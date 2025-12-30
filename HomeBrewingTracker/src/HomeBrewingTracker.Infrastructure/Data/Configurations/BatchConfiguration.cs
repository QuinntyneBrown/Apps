// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeBrewingTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Batch entity.
/// </summary>
public class BatchConfiguration : IEntityTypeConfiguration<Batch>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Batch> builder)
    {
        builder.ToTable("Batches");

        builder.HasKey(x => x.BatchId);

        builder.Property(x => x.BatchId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.RecipeId)
            .IsRequired();

        builder.Property(x => x.BatchNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.BrewDate)
            .IsRequired();

        builder.Property(x => x.BottlingDate);

        builder.Property(x => x.ActualOriginalGravity)
            .HasColumnType("decimal(5,3)");

        builder.Property(x => x.ActualFinalGravity)
            .HasColumnType("decimal(5,3)");

        builder.Property(x => x.ActualABV)
            .HasColumnType("decimal(5,2)");

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RecipeId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.BrewDate);
    }
}
