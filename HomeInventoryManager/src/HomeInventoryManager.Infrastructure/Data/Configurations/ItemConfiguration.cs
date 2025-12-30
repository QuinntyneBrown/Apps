// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeInventoryManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeInventoryManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Item entity.
/// </summary>
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");

        builder.HasKey(x => x.ItemId);

        builder.Property(x => x.ItemId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.Room)
            .IsRequired();

        builder.Property(x => x.Brand)
            .HasMaxLength(100);

        builder.Property(x => x.ModelNumber)
            .HasMaxLength(100);

        builder.Property(x => x.SerialNumber)
            .HasMaxLength(100);

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.CurrentValue)
            .HasPrecision(18, 2);

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(x => x.PhotoUrl)
            .HasMaxLength(500);

        builder.Property(x => x.ReceiptUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasMany(x => x.ValueEstimates)
            .WithOne(x => x.Item)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.Room);
        builder.HasIndex(x => new { x.UserId, x.Category });
        builder.HasIndex(x => new { x.UserId, x.Room });
    }
}
