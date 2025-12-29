// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnniversaryBirthdayReminder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the ImportantDate entity.
/// </summary>
public class ImportantDateConfiguration : IEntityTypeConfiguration<ImportantDate>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<ImportantDate> builder)
    {
        builder.ToTable("ImportantDates");

        builder.HasKey(x => x.ImportantDateId);

        builder.Property(x => x.ImportantDateId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.PersonName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.DateType)
            .IsRequired();

        builder.Property(x => x.DateValue)
            .IsRequired();

        builder.Property(x => x.RecurrencePattern)
            .IsRequired();

        builder.Property(x => x.Relationship)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Reminders)
            .WithOne(x => x.ImportantDate)
            .HasForeignKey(x => x.ImportantDateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Gifts)
            .WithOne(x => x.ImportantDate)
            .HasForeignKey(x => x.ImportantDateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Celebrations)
            .WithOne(x => x.ImportantDate)
            .HasForeignKey(x => x.ImportantDateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.DateValue);
        builder.HasIndex(x => new { x.UserId, x.IsActive });
    }
}
