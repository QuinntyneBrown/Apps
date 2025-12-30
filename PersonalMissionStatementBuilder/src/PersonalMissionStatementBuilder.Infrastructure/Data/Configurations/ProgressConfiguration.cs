// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalMissionStatementBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalMissionStatementBuilder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Progress entity.
/// </summary>
public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Progress> builder)
    {
        builder.ToTable("Progresses");

        builder.HasKey(x => x.ProgressId);

        builder.Property(x => x.ProgressId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.GoalId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ProgressDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.CompletionPercentage)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.GoalId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => new { x.GoalId, x.ProgressDate });
    }
}
