// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using StressMoodTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StressMoodTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Journal entity.
/// </summary>
public class JournalConfiguration : IEntityTypeConfiguration<Journal>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Journal> builder)
    {
        builder.ToTable("Journals");

        builder.HasKey(x => x.JournalId);

        builder.Property(x => x.JournalId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.EntryDate)
            .IsRequired();

        builder.Property(x => x.Tags)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.EntryDate);
        builder.HasIndex(x => new { x.UserId, x.EntryDate });
    }
}
