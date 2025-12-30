// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeImprovementProjectManager.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Contractor entity.
/// </summary>
public class ContractorConfiguration : IEntityTypeConfiguration<Contractor>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Contractor> builder)
    {
        builder.ToTable("Contractors");

        builder.HasKey(x => x.ContractorId);

        builder.Property(x => x.ContractorId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Trade)
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(x => x.Email)
            .HasMaxLength(200);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ProjectId);
        builder.HasIndex(x => x.Name);
    }
}
