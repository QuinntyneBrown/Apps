// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RoadsideAssistanceInfoHub.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RoadsideAssistanceInfoHub.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the InsuranceInfo entity.
/// </summary>
public class InsuranceInfoConfiguration : IEntityTypeConfiguration<InsuranceInfo>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<InsuranceInfo> builder)
    {
        builder.ToTable("InsuranceInfos");

        builder.HasKey(x => x.InsuranceInfoId);

        builder.Property(x => x.InsuranceInfoId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.VehicleId)
            .IsRequired();

        builder.Property(x => x.InsuranceCompany)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.PolicyNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PolicyHolder)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.PolicyStartDate)
            .IsRequired();

        builder.Property(x => x.PolicyEndDate)
            .IsRequired();

        builder.Property(x => x.AgentName)
            .HasMaxLength(200);

        builder.Property(x => x.AgentPhone)
            .HasMaxLength(20);

        builder.Property(x => x.CompanyPhone)
            .HasMaxLength(20);

        builder.Property(x => x.ClaimsPhone)
            .HasMaxLength(20);

        builder.Property(x => x.CoverageType)
            .HasMaxLength(100);

        builder.Property(x => x.Deductible)
            .HasPrecision(18, 2);

        builder.Property(x => x.Premium)
            .HasPrecision(18, 2);

        builder.Property(x => x.IncludesRoadsideAssistance)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.PolicyNumber);
        builder.HasIndex(x => x.PolicyEndDate);
    }
}
