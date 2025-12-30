// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WineCellarInventory.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WineCellarInventory.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the DrinkingWindow entity.
/// </summary>
public class DrinkingWindowConfiguration : IEntityTypeConfiguration<DrinkingWindow>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DrinkingWindow> builder)
    {
        builder.ToTable("DrinkingWindows");

        builder.HasKey(x => x.DrinkingWindowId);

        builder.Property(x => x.DrinkingWindowId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.WineId)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.WineId);
        builder.HasIndex(x => x.StartDate);
        builder.HasIndex(x => x.EndDate);
    }
}
