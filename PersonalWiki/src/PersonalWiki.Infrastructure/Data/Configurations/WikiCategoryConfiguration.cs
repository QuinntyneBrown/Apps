// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalWiki.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalWiki.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the WikiCategory entity.
/// </summary>
public class WikiCategoryConfiguration : IEntityTypeConfiguration<WikiCategory>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WikiCategory> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(x => x.WikiCategoryId);

        builder.Property(x => x.WikiCategoryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Icon)
            .HasMaxLength(50);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Pages)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.ChildCategories)
            .WithOne(x => x.ParentCategory)
            .HasForeignKey(x => x.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ParentCategoryId);
    }
}
