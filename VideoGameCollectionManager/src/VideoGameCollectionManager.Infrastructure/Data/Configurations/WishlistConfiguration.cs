// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VideoGameCollectionManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VideoGameCollectionManager.Infrastructure;

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
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Priority)
            .IsRequired()
            .HasDefaultValue(3);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsAcquired)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.IsAcquired);
        builder.HasIndex(x => new { x.UserId, x.IsAcquired });
        builder.HasIndex(x => new { x.UserId, x.Priority });
    }
}
