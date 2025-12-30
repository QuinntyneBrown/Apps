// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasswordAccountAuditor.Infrastructure;

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

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.AccountName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.WebsiteUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.SecurityLevel)
            .IsRequired();

        builder.Property(x => x.HasTwoFactorAuth)
            .IsRequired();

        builder.Property(x => x.LastPasswordChange);

        builder.Property(x => x.LastAccessDate);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.AccountName);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.SecurityLevel);
    }
}
