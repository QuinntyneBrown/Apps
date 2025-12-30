// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MealPrepPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPrepPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the GroceryList entity.
/// </summary>
public class GroceryListConfiguration : IEntityTypeConfiguration<GroceryList>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<GroceryList> builder)
    {
        builder.ToTable("GroceryLists");

        builder.HasKey(x => x.GroceryListId);

        builder.Property(x => x.GroceryListId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.MealPlanId);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Items)
            .IsRequired();

        builder.Property(x => x.ShoppingDate);

        builder.Property(x => x.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.EstimatedCost)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ShoppingDate);
        builder.HasIndex(x => new { x.UserId, x.IsCompleted });
    }
}
