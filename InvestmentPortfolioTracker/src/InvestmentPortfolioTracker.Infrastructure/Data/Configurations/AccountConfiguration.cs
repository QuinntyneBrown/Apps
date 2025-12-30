// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPortfolioTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Account entity.
/// </summary>
public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(x => x.AccountId);

        builder.Property(x => x.AccountId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.AccountType)
            .IsRequired();

        builder.Property(x => x.Institution)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.AccountNumber)
            .HasMaxLength(100);

        builder.Property(x => x.CurrentBalance)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.OpenedDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.AccountType);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.Institution);
    }
}
