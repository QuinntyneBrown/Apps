// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GroceryShoppingListApp.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the GroceryItem entity.
/// </summary>
public class GroceryItemConfiguration : IEntityTypeConfiguration<GroceryItem>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<GroceryItem> builder)
    {
        builder.ToTable("GroceryItems");

        builder.HasKey(x => x.GroceryItemId);

        builder.Property(x => x.GroceryItemId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.IsChecked)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.GroceryListId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.IsChecked);
    }
}
