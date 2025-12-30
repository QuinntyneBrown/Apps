// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SalaryCompensationTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalaryCompensationTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Benefit entity.
/// </summary>
public class BenefitConfiguration : IEntityTypeConfiguration<Benefit>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Benefit> builder)
    {
        builder.ToTable("Benefits");

        builder.HasKey(x => x.BenefitId);

        builder.Property(x => x.BenefitId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CompensationId);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.EstimatedValue)
            .HasPrecision(18, 2);

        builder.Property(x => x.EmployerContribution)
            .HasPrecision(18, 2);

        builder.Property(x => x.EmployeeContribution)
            .HasPrecision(18, 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CompensationId);
        builder.HasIndex(x => x.Category);
    }
}
