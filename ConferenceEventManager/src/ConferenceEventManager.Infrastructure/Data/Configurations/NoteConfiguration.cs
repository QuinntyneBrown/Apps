// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceEventManager.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Note"/> entity.
/// </summary>
public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    /// <summary>
    /// Configures the entity of type <see cref="Note"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.NoteId);

        builder.Property(n => n.EventId)
            .IsRequired();

        builder.Property(n => n.Content)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(n => n.Category)
            .HasMaxLength(100);

        builder.Property(n => n.Tags)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
            .HasMaxLength(500);

        builder.Property(n => n.CreatedAt)
            .IsRequired();

        builder.HasOne(n => n.Event)
            .WithMany(e => e.EventNotes)
            .HasForeignKey(n => n.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
