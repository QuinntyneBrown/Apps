// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LifeAdminDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeAdminDashboard.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Deadline entity.
/// </summary>
public class DeadlineConfiguration : IEntityTypeConfiguration<Deadline>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Deadline> builder)
    {
        builder.ToTable("Deadlines");

        builder.HasKey(x => x.DeadlineId);

        builder.Property(x => x.DeadlineId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.DeadlineDateTime)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasMaxLength(100);

        builder.Property(x => x.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.RemindersEnabled)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.ReminderDaysAdvance)
            .IsRequired()
            .HasDefaultValue(7);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.DeadlineDateTime);
        builder.HasIndex(x => new { x.UserId, x.IsCompleted });
        builder.HasIndex(x => new { x.UserId, x.Category });
    }
}
