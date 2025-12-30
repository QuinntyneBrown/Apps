// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InjuryPreventionRecoveryTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Injury entity.
/// </summary>
public class InjuryConfiguration : IEntityTypeConfiguration<Injury>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Injury> builder)
    {
        builder.ToTable("Injuries");

        builder.HasKey(x => x.InjuryId);

        builder.Property(x => x.InjuryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.InjuryType)
            .IsRequired();

        builder.Property(x => x.Severity)
            .IsRequired();

        builder.Property(x => x.BodyPart)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.InjuryDate)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.Diagnosis)
            .HasMaxLength(1000);

        builder.Property(x => x.ExpectedRecoveryDays);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ProgressPercentage)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.InjuryDate);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.Severity);
    }
}
