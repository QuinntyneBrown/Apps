// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GiftIdeaTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftIdeaTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Purchase entity.
/// </summary>
public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchases");

        builder.HasKey(x => x.PurchaseId);

        builder.Property(x => x.PurchaseId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.GiftIdeaId)
            .IsRequired();

        builder.Property(x => x.PurchaseDate)
            .IsRequired();

        builder.Property(x => x.ActualPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Store)
            .HasMaxLength(200);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.GiftIdeaId);
        builder.HasIndex(x => x.PurchaseDate);
    }
}
