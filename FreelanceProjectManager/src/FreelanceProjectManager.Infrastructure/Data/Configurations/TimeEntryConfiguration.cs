// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelanceProjectManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the TimeEntry entity.
/// </summary>
public class TimeEntryConfiguration : IEntityTypeConfiguration<TimeEntry>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TimeEntry> builder)
    {
        builder.ToTable("TimeEntries");

        builder.HasKey(x => x.TimeEntryId);

        builder.Property(x => x.TimeEntryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ProjectId)
            .IsRequired();

        builder.Property(x => x.WorkDate)
            .IsRequired();

        builder.Property(x => x.Hours)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.IsBillable)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.IsInvoiced)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ProjectId);
        builder.HasIndex(x => x.WorkDate);
        builder.HasIndex(x => new { x.ProjectId, x.IsInvoiced });
    }
}
