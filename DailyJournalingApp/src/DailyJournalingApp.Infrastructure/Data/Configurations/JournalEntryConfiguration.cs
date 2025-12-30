// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DailyJournalingApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyJournalingApp.Infrastructure;

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
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(10000);

        builder.Property(x => x.EntryDate)
            .IsRequired();

        builder.Property(x => x.Mood)
            .IsRequired();

        builder.Property(x => x.PromptId);

        builder.Property(x => x.Tags)
            .HasMaxLength(500);

        builder.Property(x => x.IsFavorite)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasOne(x => x.Prompt)
            .WithMany(p => p.JournalEntries)
            .HasForeignKey(x => x.PromptId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.EntryDate);
        builder.HasIndex(x => x.Mood);
        builder.HasIndex(x => x.IsFavorite);
        builder.HasIndex(x => x.PromptId);
    }
}
