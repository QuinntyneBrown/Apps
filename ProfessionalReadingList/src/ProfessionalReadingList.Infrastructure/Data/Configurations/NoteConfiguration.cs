// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfessionalReadingList.Core;

namespace ProfessionalReadingList.Infrastructure;

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

        builder.Property(x => x.ResourceId)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(x => x.PageReference)
            .HasMaxLength(200);

        builder.Property(x => x.Quote)
            .HasMaxLength(2000);

        builder.Property(x => x.Tags)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ResourceId);

        builder.HasOne(x => x.Resource)
            .WithMany(x => x.ResourceNotes)
            .HasForeignKey(x => x.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
