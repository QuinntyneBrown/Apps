// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusSessionTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Distraction entity.
/// </summary>
public class DistractionConfiguration : IEntityTypeConfiguration<Distraction>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Distraction> builder)
    {
        builder.ToTable("Distractions");

        builder.HasKey(x => x.DistractionId);

        builder.Property(x => x.DistractionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FocusSessionId)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.OccurredAt)
            .IsRequired();

        builder.Property(x => x.DurationMinutes)
            .HasPrecision(5, 2);

        builder.Property(x => x.IsInternal)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.FocusSessionId);
        builder.HasIndex(x => x.OccurredAt);
    }
}
