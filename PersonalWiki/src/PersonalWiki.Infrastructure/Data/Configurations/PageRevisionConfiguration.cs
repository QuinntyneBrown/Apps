// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalWiki.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalWiki.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the PageRevision entity.
/// </summary>
public class PageRevisionConfiguration : IEntityTypeConfiguration<PageRevision>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<PageRevision> builder)
    {
        builder.ToTable("Revisions");

        builder.HasKey(x => x.PageRevisionId);

        builder.Property(x => x.PageRevisionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.WikiPageId)
            .IsRequired();

        builder.Property(x => x.Version)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(50000)
            .IsRequired();

        builder.Property(x => x.ChangeSummary)
            .HasMaxLength(500);

        builder.Property(x => x.RevisedBy)
            .HasMaxLength(200);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.WikiPageId);
        builder.HasIndex(x => new { x.WikiPageId, x.Version });
    }
}
