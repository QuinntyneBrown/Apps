// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BucketListManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BucketListManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the BucketListItem entity.
/// </summary>
public class BucketListItemConfiguration : IEntityTypeConfiguration<BucketListItem>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<BucketListItem> builder)
    {
        builder.ToTable("BucketListItems");

        builder.HasKey(x => x.BucketListItemId);

        builder.Property(x => x.BucketListItemId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.TargetDate);

        builder.Property(x => x.CompletedDate);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.Priority);
        builder.HasIndex(x => x.Status);
    }
}
