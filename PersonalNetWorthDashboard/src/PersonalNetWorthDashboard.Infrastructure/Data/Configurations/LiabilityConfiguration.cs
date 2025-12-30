// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalNetWorthDashboard.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Liability entity.
/// </summary>
public class LiabilityConfiguration : IEntityTypeConfiguration<Liability>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Liability> builder)
    {
        builder.ToTable("Liabilities");

        builder.HasKey(x => x.LiabilityId);

        builder.Property(x => x.LiabilityId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.LiabilityType)
            .IsRequired();

        builder.Property(x => x.CurrentBalance)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.OriginalAmount)
            .HasPrecision(18, 2);

        builder.Property(x => x.InterestRate)
            .HasPrecision(5, 2);

        builder.Property(x => x.MonthlyPayment)
            .HasPrecision(18, 2);

        builder.Property(x => x.Creditor)
            .HasMaxLength(200);

        builder.Property(x => x.AccountNumber)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.LastUpdated)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(x => x.LiabilityType);
        builder.HasIndex(x => x.IsActive);
    }
}
