// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WineCellarInventory.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WineCellarInventory.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the TastingNote entity.
/// </summary>
public class TastingNoteConfiguration : IEntityTypeConfiguration<TastingNote>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TastingNote> builder)
    {
        builder.ToTable("TastingNotes");

        builder.HasKey(x => x.TastingNoteId);

        builder.Property(x => x.TastingNoteId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.WineId)
            .IsRequired();

        builder.Property(x => x.TastingDate)
            .IsRequired();

        builder.Property(x => x.Rating)
            .IsRequired();

        builder.Property(x => x.Appearance)
            .HasMaxLength(500);

        builder.Property(x => x.Aroma)
            .HasMaxLength(500);

        builder.Property(x => x.Taste)
            .HasMaxLength(500);

        builder.Property(x => x.Finish)
            .HasMaxLength(500);

        builder.Property(x => x.OverallImpression)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.WineId);
        builder.HasIndex(x => x.TastingDate);
        builder.HasIndex(x => x.Rating);
    }
}
