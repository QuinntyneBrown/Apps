// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LifeAdminDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeAdminDashboard.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the AdminTask entity.
/// </summary>
public class AdminTaskConfiguration : IEntityTypeConfiguration<AdminTask>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<AdminTask> builder)
    {
        builder.ToTable("AdminTasks");

        builder.HasKey(x => x.AdminTaskId);

        builder.Property(x => x.AdminTaskId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.IsCompleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.IsRecurring)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.RecurrencePattern)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.DueDate);
        builder.HasIndex(x => new { x.UserId, x.IsCompleted });
        builder.HasIndex(x => new { x.UserId, x.Category });
        builder.HasIndex(x => new { x.UserId, x.Priority });
    }
}
