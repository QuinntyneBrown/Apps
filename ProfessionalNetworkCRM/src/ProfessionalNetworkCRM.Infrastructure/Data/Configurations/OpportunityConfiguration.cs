// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfessionalNetworkCRM.Core.Model.OpportunityAggregate;

namespace ProfessionalNetworkCRM.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Core configuration for the Opportunity entity.
/// </summary>
public class OpportunityConfiguration : IEntityTypeConfiguration<Opportunity>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Opportunity> builder)
    {
        builder.ToTable("Opportunities");

        builder.HasKey(x => x.OpportunityId);

        builder.Property(x => x.OpportunityId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ContactId)
            .IsRequired();

        builder.Property(x => x.TenantId)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ContactId);
        builder.HasIndex(x => x.TenantId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CreatedAt);
    }
}
