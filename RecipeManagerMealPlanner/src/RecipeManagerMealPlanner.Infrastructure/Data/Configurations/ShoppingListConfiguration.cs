// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RecipeManagerMealPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipeManagerMealPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ShoppingList entity.
/// </summary>
public class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ShoppingList> builder)
    {
        builder.ToTable("ShoppingLists");

        builder.HasKey(x => x.ShoppingListId);

        builder.Property(x => x.ShoppingListId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Items)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CompletedDate);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CreatedDate);
        builder.HasIndex(x => new { x.UserId, x.IsCompleted });
    }
}
