// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SalaryCompensationTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalaryCompensationTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the MarketComparison entity.
/// </summary>
public class MarketComparisonConfiguration : IEntityTypeConfiguration<MarketComparison>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<MarketComparison> builder)
    {
        builder.ToTable("MarketComparisons");

        builder.HasKey(x => x.MarketComparisonId);

        builder.Property(x => x.MarketComparisonId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.JobTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Location)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.ExperienceLevel)
            .HasMaxLength(100);

        builder.Property(x => x.MinSalary)
            .HasPrecision(18, 2);

        builder.Property(x => x.MaxSalary)
            .HasPrecision(18, 2);

        builder.Property(x => x.MedianSalary)
            .HasPrecision(18, 2);

        builder.Property(x => x.DataSource)
            .HasMaxLength(100);

        builder.Property(x => x.ComparisonDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.JobTitle);
        builder.HasIndex(x => x.Location);
        builder.HasIndex(x => x.ComparisonDate);
    }
}
