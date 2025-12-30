// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using JobSearchOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSearchOrganizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Offer entity.
/// </summary>
public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers");

        builder.HasKey(x => x.OfferId);

        builder.Property(x => x.OfferId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ApplicationId)
            .IsRequired();

        builder.Property(x => x.Salary)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Bonus)
            .HasPrecision(18, 2);

        builder.Property(x => x.Equity)
            .HasMaxLength(500);

        builder.Property(x => x.Benefits)
            .HasMaxLength(2000);

        builder.Property(x => x.VacationDays);

        builder.Property(x => x.OfferDate)
            .IsRequired();

        builder.Property(x => x.ExpirationDate);

        builder.Property(x => x.IsAccepted)
            .IsRequired();

        builder.Property(x => x.DecisionDate);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.ApplicationId);
        builder.HasIndex(x => x.OfferDate);
        builder.HasIndex(x => x.IsAccepted);
    }
}
