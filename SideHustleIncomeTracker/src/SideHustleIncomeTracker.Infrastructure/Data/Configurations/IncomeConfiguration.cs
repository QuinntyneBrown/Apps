// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SideHustleIncomeTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SideHustleIncomeTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Income entity.
/// </summary>
public class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.ToTable("Incomes");

        builder.HasKey(x => x.IncomeId);

        builder.Property(x => x.IncomeId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.BusinessId)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.IncomeDate)
            .IsRequired();

        builder.Property(x => x.Client)
            .HasMaxLength(200);

        builder.Property(x => x.InvoiceNumber)
            .HasMaxLength(100);

        builder.Property(x => x.IsPaid)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.BusinessId);
        builder.HasIndex(x => x.IncomeDate);
        builder.HasIndex(x => x.IsPaid);
        builder.HasIndex(x => x.InvoiceNumber);
    }
}
