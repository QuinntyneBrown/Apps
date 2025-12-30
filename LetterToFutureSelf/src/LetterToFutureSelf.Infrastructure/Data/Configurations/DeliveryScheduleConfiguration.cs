// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetterToFutureSelf.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the DeliverySchedule entity.
/// </summary>
public class DeliveryScheduleConfiguration : IEntityTypeConfiguration<DeliverySchedule>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DeliverySchedule> builder)
    {
        builder.ToTable("DeliverySchedules");

        builder.HasKey(x => x.DeliveryScheduleId);

        builder.Property(x => x.DeliveryScheduleId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.LetterId)
            .IsRequired();

        builder.Property(x => x.ScheduledDateTime)
            .IsRequired();

        builder.Property(x => x.DeliveryMethod)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.RecipientContact)
            .HasMaxLength(200);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.LetterId);
        builder.HasIndex(x => x.ScheduledDateTime);
        builder.HasIndex(x => new { x.LetterId, x.IsActive });
    }
}
