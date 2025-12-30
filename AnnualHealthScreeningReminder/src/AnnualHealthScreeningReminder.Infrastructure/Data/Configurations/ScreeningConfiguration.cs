// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnnualHealthScreeningReminder.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Screening entity.
/// </summary>
public class ScreeningConfiguration : IEntityTypeConfiguration<Screening>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Screening> builder)
    {
        builder.ToTable("Screenings");

        builder.HasKey(x => x.ScreeningId);

        builder.Property(x => x.ScreeningId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ScreeningType)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.RecommendedFrequencyMonths)
            .IsRequired();

        builder.Property(x => x.Provider)
            .HasMaxLength(200);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Appointments)
            .WithOne(x => x.Screening)
            .HasForeignKey(x => x.ScreeningId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.IsDueSoon());

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.NextDueDate);
        builder.HasIndex(x => new { x.UserId, x.ScreeningType });
    }
}
