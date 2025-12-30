// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RecipeManagerMealPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipeManagerMealPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Ingredient entity.
/// </summary>
public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.ToTable("Ingredients");

        builder.HasKey(x => x.IngredientId);

        builder.Property(x => x.IngredientId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.RecipeId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Unit)
            .HasMaxLength(50);

        builder.Property(x => x.Notes)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.RecipeId);
    }
}
