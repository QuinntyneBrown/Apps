// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfessionalNetworkCRM.Core.Model.IntroductionAggregate;

namespace ProfessionalNetworkCRM.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Core configuration for the Introduction entity.
/// </summary>
public class IntroductionConfiguration : IEntityTypeConfiguration<Introduction>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Introduction> builder)
    {
        builder.ToTable("Introductions");

        builder.HasKey(x => x.IntroductionId);

        builder.Property(x => x.IntroductionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FromContactId)
            .IsRequired();

        builder.Property(x => x.ToContactId)
            .IsRequired();

        builder.Property(x => x.TenantId)
            .IsRequired();

        builder.Property(x => x.Purpose)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.FromContactId);
        builder.HasIndex(x => x.ToContactId);
        builder.HasIndex(x => x.TenantId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CreatedAt);
    }
}
