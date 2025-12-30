// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RecipeManagerMealPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipeManagerMealPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the MealPlan entity.
/// </summary>
public class MealPlanConfiguration : IEntityTypeConfiguration<MealPlan>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<MealPlan> builder)
    {
        builder.ToTable("MealPlans");

        builder.HasKey(x => x.MealPlanId);

        builder.Property(x => x.MealPlanId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.MealDate)
            .IsRequired();

        builder.Property(x => x.MealType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.RecipeId);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsPrepared)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MealDate);
        builder.HasIndex(x => new { x.UserId, x.MealDate });
    }
}
