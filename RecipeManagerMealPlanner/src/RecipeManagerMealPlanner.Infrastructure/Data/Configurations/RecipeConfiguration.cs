// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RecipeManagerMealPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipeManagerMealPlanner.Infrastructure;

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
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Cuisine)
            .IsRequired();

        builder.Property(x => x.DifficultyLevel)
            .IsRequired();

        builder.Property(x => x.PrepTimeMinutes)
            .IsRequired();

        builder.Property(x => x.CookTimeMinutes)
            .IsRequired();

        builder.Property(x => x.Servings)
            .IsRequired();

        builder.Property(x => x.Instructions)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(x => x.PhotoUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Source)
            .HasMaxLength(200);

        builder.Property(x => x.Rating);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsFavorite)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Ingredients)
            .WithOne(x => x.Recipe)
            .HasForeignKey(x => x.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.MealPlans)
            .WithOne(x => x.Recipe)
            .HasForeignKey(x => x.RecipeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => new { x.UserId, x.IsFavorite });
    }
}
