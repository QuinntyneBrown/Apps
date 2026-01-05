// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfessionalNetworkCRM.Core.Models.ReferralAggregate;

namespace ProfessionalNetworkCRM.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Core configuration for the Referral entity.
/// </summary>
public class ReferralConfiguration : IEntityTypeConfiguration<Referral>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Referral> builder)
    {
        builder.ToTable("Referrals");

        builder.HasKey(x => x.ReferralId);

        builder.Property(x => x.ReferralId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SourceContactId)
            .IsRequired();

        builder.Property(x => x.TenantId)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Outcome)
            .HasMaxLength(1000);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.ThankYouSent)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.SourceContactId);
        builder.HasIndex(x => x.TenantId);
        builder.HasIndex(x => x.ThankYouSent);
        builder.HasIndex(x => x.CreatedAt);
    }
}
