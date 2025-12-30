// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfessionalReadingList.Core;

namespace ProfessionalReadingList.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ReadingProgress entity.
/// </summary>
public class ReadingProgressConfiguration : IEntityTypeConfiguration<ReadingProgress>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ReadingProgress> builder)
    {
        builder.ToTable("ReadingProgress");

        builder.HasKey(x => x.ReadingProgressId);

        builder.Property(x => x.ReadingProgressId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ResourceId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ProgressPercentage)
            .IsRequired();

        builder.Property(x => x.Review)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ResourceId)
            .IsUnique();
        builder.HasIndex(x => x.Status);
    }
}
