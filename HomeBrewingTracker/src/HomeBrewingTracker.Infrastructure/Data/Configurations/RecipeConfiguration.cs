// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeBrewingTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Recipe entity.
/// </summary>
public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("Recipes");

        builder.HasKey(x => x.RecipeId);

        builder.Property(x => x.RecipeId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.BeerStyle)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.OriginalGravity)
            .HasColumnType("decimal(5,3)");

        builder.Property(x => x.FinalGravity)
            .HasColumnType("decimal(5,3)");

        builder.Property(x => x.ABV)
            .HasColumnType("decimal(5,2)");

        builder.Property(x => x.IBU);

        builder.Property(x => x.BatchSize)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(x => x.Ingredients)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Instructions)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.IsFavorite)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.BeerStyle);
        builder.HasIndex(x => x.IsFavorite);
    }
}
