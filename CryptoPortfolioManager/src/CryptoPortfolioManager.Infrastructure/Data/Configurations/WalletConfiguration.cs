// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoPortfolioManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Wallet entity.
/// </summary>
public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");

        builder.HasKey(x => x.WalletId);

        builder.Property(x => x.WalletId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Address)
            .HasMaxLength(500);

        builder.Property(x => x.WalletType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasMany(x => x.Holdings)
            .WithOne(h => h.Wallet)
            .HasForeignKey(h => h.WalletId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.WalletType);
        builder.HasIndex(x => x.IsActive);
    }
}
