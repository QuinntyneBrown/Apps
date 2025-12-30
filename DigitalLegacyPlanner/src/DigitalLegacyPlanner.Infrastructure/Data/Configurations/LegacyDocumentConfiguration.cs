// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalLegacyPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the LegacyDocument entity.
/// </summary>
public class LegacyDocumentConfiguration : IEntityTypeConfiguration<LegacyDocument>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<LegacyDocument> builder)
    {
        builder.ToTable("LegacyDocuments");

        builder.HasKey(x => x.LegacyDocumentId);

        builder.Property(x => x.LegacyDocumentId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.DocumentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.FilePath)
            .HasMaxLength(500);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.PhysicalLocation)
            .HasMaxLength(500);

        builder.Property(x => x.AccessGrantedTo)
            .HasMaxLength(500);

        builder.Property(x => x.IsEncrypted)
            .IsRequired();

        builder.Property(x => x.LastReviewedAt);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.DocumentType);
        builder.HasIndex(x => x.LastReviewedAt);
    }
}
