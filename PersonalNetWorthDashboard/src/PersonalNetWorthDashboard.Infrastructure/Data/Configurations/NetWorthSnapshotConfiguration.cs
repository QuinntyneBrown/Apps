// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalNetWorthDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalNetWorthDashboard.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the NetWorthSnapshot entity.
/// </summary>
public class NetWorthSnapshotConfiguration : IEntityTypeConfiguration<NetWorthSnapshot>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<NetWorthSnapshot> builder)
    {
        builder.ToTable("NetWorthSnapshots");

        builder.HasKey(x => x.NetWorthSnapshotId);

        builder.Property(x => x.NetWorthSnapshotId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SnapshotDate)
            .IsRequired();

        builder.Property(x => x.TotalAssets)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.TotalLiabilities)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.NetWorth)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.SnapshotDate);
    }
}
