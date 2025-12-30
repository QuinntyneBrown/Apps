// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasswordAccountAuditor.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the BreachAlert entity.
/// </summary>
public class BreachAlertConfiguration : IEntityTypeConfiguration<BreachAlert>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<BreachAlert> builder)
    {
        builder.ToTable("BreachAlerts");

        builder.HasKey(x => x.BreachAlertId);

        builder.Property(x => x.BreachAlertId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AccountId)
            .IsRequired();

        builder.Property(x => x.Severity)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.DetectedDate)
            .IsRequired();

        builder.Property(x => x.BreachDate);

        builder.Property(x => x.Source)
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.DataCompromised)
            .HasMaxLength(1000);

        builder.Property(x => x.RecommendedActions)
            .HasMaxLength(2000);

        builder.Property(x => x.AcknowledgedAt);

        builder.Property(x => x.ResolvedAt);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.AccountId);
        builder.HasIndex(x => x.Severity);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.DetectedDate);
    }
}
