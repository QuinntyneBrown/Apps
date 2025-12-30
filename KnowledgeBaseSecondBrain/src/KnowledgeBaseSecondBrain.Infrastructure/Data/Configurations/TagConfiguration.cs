// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KnowledgeBaseSecondBrain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeBaseSecondBrain.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Tag entity.
/// </summary>
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(x => x.TagId);

        builder.Property(x => x.TagId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Color)
            .HasMaxLength(20);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.UserId, x.Name })
            .IsUnique();
    }
}
