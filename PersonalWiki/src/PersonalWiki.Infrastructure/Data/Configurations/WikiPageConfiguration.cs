// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalWiki.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalWiki.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the WikiPage entity.
/// </summary>
public class WikiPageConfiguration : IEntityTypeConfiguration<WikiPage>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WikiPage> builder)
    {
        builder.ToTable("Pages");

        builder.HasKey(x => x.WikiPageId);

        builder.Property(x => x.WikiPageId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(50000)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Version)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(x => x.IsFeatured)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.ViewCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.LastModifiedAt)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Revisions)
            .WithOne(x => x.Page)
            .HasForeignKey(x => x.WikiPageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.OutgoingLinks)
            .WithOne(x => x.SourcePage)
            .HasForeignKey(x => x.SourcePageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.IncomingLinks)
            .WithOne(x => x.TargetPage)
            .HasForeignKey(x => x.TargetPageId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => new { x.UserId, x.Status });
    }
}
