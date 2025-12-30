// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BucketListManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BucketListManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Memory entity.
/// </summary>
public class MemoryConfiguration : IEntityTypeConfiguration<Memory>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Memory> builder)
    {
        builder.ToTable("Memories");

        builder.HasKey(x => x.MemoryId);

        builder.Property(x => x.MemoryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.BucketListItemId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.MemoryDate)
            .IsRequired();

        builder.Property(x => x.PhotoUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.BucketListItemId);
        builder.HasIndex(x => x.MemoryDate);
    }
}
