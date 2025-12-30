// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalLegacyPlanner.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the DigitalAccount entity.
/// </summary>
public class DigitalAccountConfiguration : IEntityTypeConfiguration<DigitalAccount>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DigitalAccount> builder)
    {
        builder.ToTable("DigitalAccounts");

        builder.HasKey(x => x.DigitalAccountId);

        builder.Property(x => x.DigitalAccountId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.AccountType)
            .IsRequired();

        builder.Property(x => x.AccountName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.PasswordHint)
            .HasMaxLength(500);

        builder.Property(x => x.Url)
            .HasMaxLength(500);

        builder.Property(x => x.DesiredAction)
            .HasMaxLength(500);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.LastUpdatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.AccountType);
        builder.HasIndex(x => x.AccountName);
    }
}
