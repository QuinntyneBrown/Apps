// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentVaultOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the DocumentCategory entity.
/// </summary>
public class DocumentCategoryConfiguration : IEntityTypeConfiguration<DocumentCategory>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DocumentCategory> builder)
    {
        builder.ToTable("DocumentCategories");

        builder.HasKey(x => x.DocumentCategoryId);

        builder.Property(x => x.DocumentCategoryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.Name);
    }
}
