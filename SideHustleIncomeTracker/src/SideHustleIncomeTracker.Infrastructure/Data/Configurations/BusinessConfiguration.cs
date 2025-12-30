// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SideHustleIncomeTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SideHustleIncomeTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Business entity.
/// </summary>
public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.ToTable("Businesses");

        builder.HasKey(x => x.BusinessId);

        builder.Property(x => x.BusinessId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.TaxId)
            .HasMaxLength(50);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.TotalIncome)
            .HasPrecision(18, 2);

        builder.Property(x => x.TotalExpenses)
            .HasPrecision(18, 2);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.StartDate);
    }
}
