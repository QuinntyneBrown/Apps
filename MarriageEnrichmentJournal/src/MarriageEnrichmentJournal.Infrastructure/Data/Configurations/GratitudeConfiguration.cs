// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MarriageEnrichmentJournal.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarriageEnrichmentJournal.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Gratitude entity.
/// </summary>
public class GratitudeConfiguration : IEntityTypeConfiguration<Gratitude>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Gratitude> builder)
    {
        builder.ToTable("Gratitudes");

        builder.HasKey(x => x.GratitudeId);

        builder.Property(x => x.GratitudeId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Text)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.GratitudeDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.GratitudeDate);
        builder.HasIndex(x => x.JournalEntryId);
    }
}
