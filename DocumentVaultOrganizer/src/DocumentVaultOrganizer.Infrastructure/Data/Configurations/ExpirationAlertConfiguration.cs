// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentVaultOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ExpirationAlert entity.
/// </summary>
public class ExpirationAlertConfiguration : IEntityTypeConfiguration<ExpirationAlert>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ExpirationAlert> builder)
    {
        builder.ToTable("ExpirationAlerts");

        builder.HasKey(x => x.ExpirationAlertId);

        builder.Property(x => x.ExpirationAlertId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.DocumentId)
            .IsRequired();

        builder.Property(x => x.AlertDate)
            .IsRequired();

        builder.Property(x => x.IsAcknowledged)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.DocumentId);
        builder.HasIndex(x => x.AlertDate);
        builder.HasIndex(x => x.IsAcknowledged);
    }
}
