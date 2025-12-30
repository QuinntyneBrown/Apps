// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleValueTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VehicleValueTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ValueAssessment entity.
/// </summary>
public class ValueAssessmentConfiguration : IEntityTypeConfiguration<ValueAssessment>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ValueAssessment> builder)
    {
        builder.ToTable("ValueAssessments");

        builder.HasKey(x => x.ValueAssessmentId);

        builder.Property(x => x.ValueAssessmentId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VehicleId)
            .IsRequired();

        builder.Property(x => x.AssessmentDate)
            .IsRequired();

        builder.Property(x => x.EstimatedValue)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.MileageAtAssessment)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ConditionGrade)
            .IsRequired();

        builder.Property(x => x.ValuationSource)
            .HasMaxLength(100);

        builder.Property(x => x.ExteriorCondition)
            .HasMaxLength(500);

        builder.Property(x => x.InteriorCondition)
            .HasMaxLength(500);

        builder.Property(x => x.MechanicalCondition)
            .HasMaxLength(500);

        builder.Property(x => x.Modifications)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
            .HasMaxLength(2000);

        builder.Property(x => x.KnownIssues)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
            .HasMaxLength(2000);

        builder.Property(x => x.DepreciationAmount)
            .HasPrecision(18, 2);

        builder.Property(x => x.DepreciationPercentage)
            .HasPrecision(5, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.AssessmentDate);
        builder.HasIndex(x => new { x.VehicleId, x.AssessmentDate });
    }
}
