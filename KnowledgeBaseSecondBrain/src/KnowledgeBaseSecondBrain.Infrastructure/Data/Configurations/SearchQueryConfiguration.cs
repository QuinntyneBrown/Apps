// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KnowledgeBaseSecondBrain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeBaseSecondBrain.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the SearchQuery entity.
/// </summary>
public class SearchQueryConfiguration : IEntityTypeConfiguration<SearchQuery>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<SearchQuery> builder)
    {
        builder.ToTable("SearchQueries");

        builder.HasKey(x => x.SearchQueryId);

        builder.Property(x => x.SearchQueryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.QueryText)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200);

        builder.Property(x => x.IsSaved)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.ExecutionCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.IsSaved });
    }
}
