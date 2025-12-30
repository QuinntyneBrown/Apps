// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalWiki.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalWiki.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the PageLink entity.
/// </summary>
public class PageLinkConfiguration : IEntityTypeConfiguration<PageLink>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<PageLink> builder)
    {
        builder.ToTable("Links");

        builder.HasKey(x => x.PageLinkId);

        builder.Property(x => x.PageLinkId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SourcePageId)
            .IsRequired();

        builder.Property(x => x.TargetPageId)
            .IsRequired();

        builder.Property(x => x.AnchorText)
            .HasMaxLength(300);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.SourcePageId);
        builder.HasIndex(x => x.TargetPageId);
        builder.HasIndex(x => new { x.SourcePageId, x.TargetPageId });
    }
}
