// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GroceryShoppingListApp.Infrastructure;

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

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.IsCompleted)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CreatedDate);
        builder.HasIndex(x => x.IsCompleted);
    }
}
