// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BookReadingTrackerLibrary.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReadingTrackerLibrary.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Wishlist entity.
/// </summary>
public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.ToTable("Wishlists");

        builder.HasKey(x => x.WishlistId);

        builder.Property(x => x.WishlistId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Author)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.ISBN)
            .HasMaxLength(20);

        builder.Property(x => x.Genre);

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.IsAcquired)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Priority);
        builder.HasIndex(x => x.IsAcquired);
    }
}
