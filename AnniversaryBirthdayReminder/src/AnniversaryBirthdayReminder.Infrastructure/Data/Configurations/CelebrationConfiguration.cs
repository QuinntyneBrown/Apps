// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnniversaryBirthdayReminder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Celebration entity.
/// </summary>
public class CelebrationConfiguration : IEntityTypeConfiguration<Celebration>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Celebration> builder)
    {
        builder.ToTable("Celebrations");

        builder.HasKey(x => x.CelebrationId);

        builder.Property(x => x.CelebrationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ImportantDateId)
            .IsRequired();

        builder.Property(x => x.CelebrationDate)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.Photos)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.Attendees)
            .HasConversion(
                v => string.Join("|||", v),
                v => v.Split("|||", StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.Property(x => x.Rating);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.HasIndex(x => x.ImportantDateId);
        builder.HasIndex(x => x.CelebrationDate);
        builder.HasIndex(x => x.Status);
    }
}
