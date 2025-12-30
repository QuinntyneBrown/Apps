// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTreeBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Story entity.
/// </summary>
public class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.ToTable("Stories");

        builder.HasKey(x => x.StoryId);

        builder.Property(x => x.StoryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PersonId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Content)
            .HasMaxLength(5000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.PersonId);
    }
}
