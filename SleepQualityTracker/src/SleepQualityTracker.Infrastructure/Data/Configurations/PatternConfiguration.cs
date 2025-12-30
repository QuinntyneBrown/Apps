// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SleepQualityTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SleepQualityTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Pattern entity.
/// </summary>
public class PatternConfiguration : IEntityTypeConfiguration<Pattern>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Pattern> builder)
    {
        builder.ToTable("Patterns");

        builder.HasKey(x => x.PatternId);

        builder.Property(x => x.PatternId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.PatternType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.ConfidenceLevel)
            .IsRequired();

        builder.Property(x => x.Insights)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.PatternType);
        builder.HasIndex(x => x.ConfidenceLevel);
    }
}
