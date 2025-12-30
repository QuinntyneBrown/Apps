// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TimeAuditTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeAuditTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the TimeBlock entity.
/// </summary>
public class TimeBlockConfiguration : IEntityTypeConfiguration<TimeBlock>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TimeBlock> builder)
    {
        builder.ToTable("TimeBlocks");

        builder.HasKey(x => x.TimeBlockId);

        builder.Property(x => x.TimeBlockId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.Tags)
            .HasMaxLength(500);

        builder.Property(x => x.IsProductive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.StartTime);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => new { x.UserId, x.StartTime });
        builder.HasIndex(x => new { x.UserId, x.Category });
    }
}
