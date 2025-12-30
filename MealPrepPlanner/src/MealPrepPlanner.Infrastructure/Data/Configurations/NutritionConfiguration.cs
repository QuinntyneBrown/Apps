// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MealPrepPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPrepPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Nutrition entity.
/// </summary>
public class NutritionConfiguration : IEntityTypeConfiguration<Nutrition>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Nutrition> builder)
    {
        builder.ToTable("Nutritions");

        builder.HasKey(x => x.NutritionId);

        builder.Property(x => x.NutritionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.RecipeId);

        builder.Property(x => x.Calories)
            .IsRequired();

        builder.Property(x => x.Protein)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Carbohydrates)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Fat)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Fiber)
            .HasPrecision(18, 2);

        builder.Property(x => x.Sugar)
            .HasPrecision(18, 2);

        builder.Property(x => x.Sodium)
            .HasPrecision(18, 2);

        builder.Property(x => x.AdditionalNutrients);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RecipeId);
    }
}
