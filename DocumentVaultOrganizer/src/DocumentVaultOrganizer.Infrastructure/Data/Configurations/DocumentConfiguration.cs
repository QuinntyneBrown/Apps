// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentVaultOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Document entity.
/// </summary>
public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(x => x.DocumentId);

        builder.Property(x => x.DocumentId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.FileUrl)
            .HasMaxLength(2000);

        builder.Property(x => x.ExpirationDate);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.ExpirationDate);
    }
}
