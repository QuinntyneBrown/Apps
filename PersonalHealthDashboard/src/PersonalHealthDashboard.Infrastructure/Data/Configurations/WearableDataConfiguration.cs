// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalHealthDashboard.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalHealthDashboard.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the WearableData entity.
/// </summary>
public class WearableDataConfiguration : IEntityTypeConfiguration<WearableData>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WearableData> builder)
    {
        builder.ToTable("WearableData");

        builder.HasKey(x => x.WearableDataId);

        builder.Property(x => x.WearableDataId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.DeviceName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.DataType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Value)
            .IsRequired();

        builder.Property(x => x.Unit)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.RecordedAt)
            .IsRequired();

        builder.Property(x => x.SyncedAt)
            .IsRequired();

        builder.Property(x => x.Metadata)
            .HasMaxLength(4000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.DeviceName);
        builder.HasIndex(x => x.DataType);
        builder.HasIndex(x => x.RecordedAt);
    }
}
