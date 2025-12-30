// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeEnergyUsageTracker.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Usage entity.
/// </summary>
public class UsageConfiguration : IEntityTypeConfiguration<Usage>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Usage> builder)
    {
        builder.ToTable("Usages");

        builder.HasKey(x => x.UsageId);

        builder.Property(x => x.UsageId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UtilityBillId)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UtilityBillId);
        builder.HasIndex(x => x.Date);
    }
}
