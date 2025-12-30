// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SalaryCompensationTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalaryCompensationTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Compensation entity.
/// </summary>
public class CompensationConfiguration : IEntityTypeConfiguration<Compensation>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Compensation> builder)
    {
        builder.ToTable("Compensations");

        builder.HasKey(x => x.CompensationId);

        builder.Property(x => x.CompensationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CompensationType)
            .IsRequired();

        builder.Property(x => x.Employer)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.JobTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.BaseSalary)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(x => x.Bonus)
            .HasPrecision(18, 2);

        builder.Property(x => x.StockValue)
            .HasPrecision(18, 2);

        builder.Property(x => x.OtherCompensation)
            .HasPrecision(18, 2);

        builder.Property(x => x.TotalCompensation)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.EffectiveDate)
            .IsRequired();

        builder.Property(x => x.EndDate);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Employer);
        builder.HasIndex(x => x.EffectiveDate);
        builder.HasIndex(x => x.CompensationType);
    }
}
