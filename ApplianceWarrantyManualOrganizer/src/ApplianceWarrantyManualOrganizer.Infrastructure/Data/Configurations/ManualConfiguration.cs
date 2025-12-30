// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplianceWarrantyManualOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Manual entity.
/// </summary>
public class ManualConfiguration : IEntityTypeConfiguration<Manual>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Manual> builder)
    {
        builder.ToTable("Manuals");

        builder.HasKey(x => x.ManualId);

        builder.Property(x => x.ManualId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ApplianceId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(300);

        builder.Property(x => x.FileUrl)
            .HasMaxLength(500);

        builder.Property(x => x.FileType)
            .HasMaxLength(50);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ApplianceId);
    }
}
