// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KnowledgeBaseSecondBrain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeBaseSecondBrain.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the NoteLink entity.
/// </summary>
public class NoteLinkConfiguration : IEntityTypeConfiguration<NoteLink>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<NoteLink> builder)
    {
        builder.ToTable("NoteLinks");

        builder.HasKey(x => x.NoteLinkId);

        builder.Property(x => x.NoteLinkId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SourceNoteId)
            .IsRequired();

        builder.Property(x => x.TargetNoteId)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.LinkType)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.SourceNoteId);
        builder.HasIndex(x => x.TargetNoteId);
        builder.HasIndex(x => new { x.SourceNoteId, x.TargetNoteId });
    }
}
