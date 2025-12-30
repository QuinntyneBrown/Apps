// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalLoanComparisonTool.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the PaymentSchedule entity.
/// </summary>
public class PaymentScheduleConfiguration : IEntityTypeConfiguration<PaymentSchedule>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<PaymentSchedule> builder)
    {
        builder.ToTable("PaymentSchedules");

        builder.HasKey(x => x.PaymentScheduleId);

        builder.Property(x => x.PaymentScheduleId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.OfferId)
            .IsRequired();

        builder.Property(x => x.PaymentNumber)
            .IsRequired();

        builder.Property(x => x.DueDate)
            .IsRequired();

        builder.Property(x => x.PaymentAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.PrincipalAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.InterestAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.RemainingBalance)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasIndex(x => x.OfferId);
        builder.HasIndex(x => x.PaymentNumber);
        builder.HasIndex(x => x.DueDate);
    }
}
