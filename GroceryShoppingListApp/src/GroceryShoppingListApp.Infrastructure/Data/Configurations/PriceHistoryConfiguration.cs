// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GroceryShoppingListApp.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the PriceHistory entity.
/// </summary>
public class PriceHistoryConfiguration : IEntityTypeConfiguration<PriceHistory>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<PriceHistory> builder)
    {
        builder.ToTable("PriceHistories");

        builder.HasKey(x => x.PriceHistoryId);

        builder.Property(x => x.PriceHistoryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.GroceryItemId)
            .IsRequired();

        builder.Property(x => x.StoreId)
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.GroceryItemId);
        builder.HasIndex(x => x.StoreId);
        builder.HasIndex(x => x.Date);
    }
}
