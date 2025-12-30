// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using StressMoodTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StressMoodTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Trigger entity.
/// </summary>
public class TriggerConfiguration : IEntityTypeConfiguration<Trigger>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Trigger> builder)
    {
        builder.ToTable("Triggers");

        builder.HasKey(x => x.TriggerId);

        builder.Property(x => x.TriggerId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.TriggerType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ImpactLevel)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.TriggerType);
        builder.HasIndex(x => new { x.UserId, x.ImpactLevel });
    }
}
