// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Api.Tests;

/// <summary>
/// Tests for Payment DTO mapping.
/// </summary>
[TestFixture]
public class PaymentDtoMappingTests
{
    [Test]
    public void ToDto_ShouldMapPaymentToPaymentDto()
    {
        // Arrange
        var payment = new Payment
        {
            PaymentId = Guid.NewGuid(),
            MortgageId = Guid.NewGuid(),
            PaymentDate = new DateTime(2024, 1, 15),
            Amount = 1347.13m,
            PrincipalAmount = 618.47m,
            InterestAmount = 728.66m,
            ExtraPrincipal = 100m,
            Notes = "Regular monthly payment"
        };

        // Act
        var dto = payment.ToDto();

        // Assert
        Assert.That(dto.PaymentId, Is.EqualTo(payment.PaymentId));
        Assert.That(dto.MortgageId, Is.EqualTo(payment.MortgageId));
        Assert.That(dto.PaymentDate, Is.EqualTo(payment.PaymentDate));
        Assert.That(dto.Amount, Is.EqualTo(payment.Amount));
        Assert.That(dto.PrincipalAmount, Is.EqualTo(payment.PrincipalAmount));
        Assert.That(dto.InterestAmount, Is.EqualTo(payment.InterestAmount));
        Assert.That(dto.ExtraPrincipal, Is.EqualTo(payment.ExtraPrincipal));
        Assert.That(dto.Notes, Is.EqualTo(payment.Notes));
    }
}
