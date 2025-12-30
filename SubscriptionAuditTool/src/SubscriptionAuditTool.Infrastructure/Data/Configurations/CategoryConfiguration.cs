// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SubscriptionAuditTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SubscriptionAuditTool.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Category entity.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(x => x.CategoryId);

        builder.Property(x => x.CategoryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.ColorCode)
            .HasMaxLength(7);

        builder.HasMany(x => x.Subscriptions)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}
