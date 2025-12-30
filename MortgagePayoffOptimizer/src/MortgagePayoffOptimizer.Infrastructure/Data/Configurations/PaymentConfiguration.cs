// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MortgagePayoffOptimizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MortgagePayoffOptimizer.Infrastructure;

/// <summary>
/// Entity Framework Core configuration for the Payment entity.
/// </summary>
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(x => x.PaymentId);

        builder.Property(x => x.PaymentId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MortgageId)
            .IsRequired();

        builder.Property(x => x.PaymentDate)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PrincipalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.InterestAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ExtraPrincipal)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Mortgage)
            .WithMany()
            .HasForeignKey(x => x.MortgageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.MortgageId);
        builder.HasIndex(x => x.PaymentDate);
        builder.HasIndex(x => new { x.MortgageId, x.PaymentDate });
    }
}
