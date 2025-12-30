// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeSavingsPlanner.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for the <see cref="Beneficiary"/> entity.
/// </summary>
public class BeneficiaryConfiguration : IEntityTypeConfiguration<Beneficiary>
{
    /// <summary>
    /// Configures the entity of type <see cref="Beneficiary"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Beneficiary> builder)
    {
        builder.HasKey(b => b.BeneficiaryId);

        builder.Property(b => b.PlanId)
            .IsRequired();

        builder.Property(b => b.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.DateOfBirth)
            .IsRequired();

        builder.Property(b => b.Relationship)
            .HasMaxLength(100);

        builder.Property(b => b.IsPrimary)
            .IsRequired();

        builder.HasOne(b => b.Plan)
            .WithMany()
            .HasForeignKey(b => b.PlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
