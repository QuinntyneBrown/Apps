// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBQGrillingRecipeBook.Infrastructure;

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
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.MeatType)
            .IsRequired();

        builder.Property(x => x.CookingMethod)
            .IsRequired();

        builder.Property(x => x.PrepTimeMinutes)
            .IsRequired();

        builder.Property(x => x.CookTimeMinutes)
            .IsRequired();

        builder.Property(x => x.Ingredients)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.Instructions)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(x => x.Servings)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsFavorite)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.CookSessions)
            .WithOne(x => x.Recipe)
            .HasForeignKey(x => x.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.GetTotalTimeMinutes());
        builder.Ignore(x => x.ToggleFavorite());

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MeatType);
        builder.HasIndex(x => x.CookingMethod);
        builder.HasIndex(x => new { x.UserId, x.IsFavorite });
    }
}
