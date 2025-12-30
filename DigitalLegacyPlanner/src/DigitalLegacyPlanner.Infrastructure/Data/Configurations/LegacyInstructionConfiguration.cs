// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalLegacyPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the LegacyInstruction entity.
/// </summary>
public class LegacyInstructionConfiguration : IEntityTypeConfiguration<LegacyInstruction>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<LegacyInstruction> builder)
    {
        builder.ToTable("LegacyInstructions");

        builder.HasKey(x => x.LegacyInstructionId);

        builder.Property(x => x.LegacyInstructionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.Category)
            .HasMaxLength(100);

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.AssignedTo)
            .HasMaxLength(200);

        builder.Property(x => x.ExecutionTiming)
            .HasMaxLength(200);

        builder.Property(x => x.LastUpdatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.Priority);
    }
}
