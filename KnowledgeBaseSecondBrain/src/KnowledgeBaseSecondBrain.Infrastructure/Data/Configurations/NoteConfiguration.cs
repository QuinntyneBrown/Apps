// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KnowledgeBaseSecondBrain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeBaseSecondBrain.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Note entity.
/// </summary>
public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("Notes");

        builder.HasKey(x => x.NoteId);

        builder.Property(x => x.NoteId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.NoteType)
            .IsRequired();

        builder.Property(x => x.IsFavorite)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.IsArchived)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.LastModifiedAt)
            .IsRequired();

        builder.HasOne(x => x.ParentNote)
            .WithMany(x => x.ChildNotes)
            .HasForeignKey(x => x.ParentNoteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Tags)
            .WithMany(x => x.Notes)
            .UsingEntity(j => j.ToTable("NoteTags"));

        builder.HasMany(x => x.OutgoingLinks)
            .WithOne(x => x.SourceNote)
            .HasForeignKey(x => x.SourceNoteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.IncomingLinks)
            .WithOne(x => x.TargetNote)
            .HasForeignKey(x => x.TargetNoteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => new { x.UserId, x.IsFavorite });
        builder.HasIndex(x => new { x.UserId, x.IsArchived });
    }
}
