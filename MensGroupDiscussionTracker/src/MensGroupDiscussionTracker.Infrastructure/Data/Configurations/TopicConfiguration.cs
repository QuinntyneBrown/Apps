// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MensGroupDiscussionTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MensGroupDiscussionTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Topic entity.
/// </summary>
public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.ToTable("Topics");

        builder.HasKey(x => x.TopicId);

        builder.Property(x => x.TopicId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MeetingId);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Description);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.DiscussionNotes);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.MeetingId);
        builder.HasIndex(x => x.Category);
    }
}
