// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeEnergyUsageTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the SavingsTip entity.
/// </summary>
public class SavingsTipConfiguration : IEntityTypeConfiguration<SavingsTip>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<SavingsTip> builder)
    {
        builder.ToTable("SavingsTips");

        builder.HasKey(x => x.SavingsTipId);

        builder.Property(x => x.SavingsTipId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.CreatedAt);
    }
}
