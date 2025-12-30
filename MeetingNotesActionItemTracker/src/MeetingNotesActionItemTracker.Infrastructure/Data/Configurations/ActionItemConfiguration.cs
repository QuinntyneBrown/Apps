// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingNotesActionItemTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ActionItem entity.
/// </summary>
public class ActionItemConfiguration : IEntityTypeConfiguration<ActionItem>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ActionItem> builder)
    {
        builder.ToTable("ActionItems");

        builder.HasKey(x => x.ActionItemId);

        builder.Property(x => x.ActionItemId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.MeetingId)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.ResponsiblePerson)
            .HasMaxLength(200);

        builder.Property(x => x.DueDate);

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CompletedDate);

        builder.Property(x => x.Notes);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MeetingId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.DueDate);
        builder.HasIndex(x => new { x.UserId, x.Status });
    }
}
