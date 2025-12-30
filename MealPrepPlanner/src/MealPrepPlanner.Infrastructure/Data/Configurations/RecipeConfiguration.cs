// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MealPrepPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPrepPlanner.Infrastructure;

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

        builder.Property(x => x.MealPlanId);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.PrepTimeMinutes)
            .IsRequired();

        builder.Property(x => x.CookTimeMinutes)
            .IsRequired();

        builder.Property(x => x.Servings)
            .IsRequired();

        builder.Property(x => x.Ingredients)
            .IsRequired();

        builder.Property(x => x.Instructions)
            .IsRequired();

        builder.Property(x => x.MealType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Tags)
            .HasMaxLength(500);

        builder.Property(x => x.IsFavorite)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MealType);
        builder.HasIndex(x => new { x.UserId, x.IsFavorite });
    }
}
