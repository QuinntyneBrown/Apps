// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MarriageEnrichmentJournal.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarriageEnrichmentJournal.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the JournalEntry entity.
/// </summary>
public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        builder.ToTable("JournalEntries");

        builder.HasKey(x => x.JournalEntryId);

        builder.Property(x => x.JournalEntryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.EntryType)
            .IsRequired();

        builder.Property(x => x.EntryDate)
            .IsRequired();

        builder.Property(x => x.IsSharedWithPartner)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.IsPrivate)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Tags)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Gratitudes)
            .WithOne(x => x.JournalEntry)
            .HasForeignKey(x => x.JournalEntryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Reflections)
            .WithOne(x => x.JournalEntry)
            .HasForeignKey(x => x.JournalEntryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.EntryDate);
        builder.HasIndex(x => new { x.UserId, x.EntryType });
        builder.HasIndex(x => new { x.UserId, x.IsPrivate });
        builder.HasIndex(x => new { x.UserId, x.IsSharedWithPartner });
    }
}
