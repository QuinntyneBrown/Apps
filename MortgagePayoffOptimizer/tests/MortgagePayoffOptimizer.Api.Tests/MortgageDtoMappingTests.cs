// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Api.Tests;

/// <summary>
/// Tests for Mortgage DTO mapping.
/// </summary>
[TestFixture]
public class MortgageDtoMappingTests
{
    [Test]
    public void ToDto_ShouldMapMortgageToMortgageDto()
    {
        // Arrange
        var mortgage = new Mortgage
        {
            MortgageId = Guid.NewGuid(),
            PropertyAddress = "123 Main St",
            Lender = "Bank of America",
            OriginalLoanAmount = 300000m,
            CurrentBalance = 250000m,
            InterestRate = 3.5m,
            LoanTermYears = 30,
            MonthlyPayment = 1347.13m,
            StartDate = new DateTime(2020, 1, 1),
            MortgageType = MortgageType.Fixed,
            IsActive = true,
            Notes = "Primary residence"
        };

        // Act
        var dto = mortgage.ToDto();

        // Assert
        Assert.That(dto.MortgageId, Is.EqualTo(mortgage.MortgageId));
        Assert.That(dto.PropertyAddress, Is.EqualTo(mortgage.PropertyAddress));
        Assert.That(dto.Lender, Is.EqualTo(mortgage.Lender));
        Assert.That(dto.OriginalLoanAmount, Is.EqualTo(mortgage.OriginalLoanAmount));
        Assert.That(dto.CurrentBalance, Is.EqualTo(mortgage.CurrentBalance));
        Assert.That(dto.InterestRate, Is.EqualTo(mortgage.InterestRate));
        Assert.That(dto.LoanTermYears, Is.EqualTo(mortgage.LoanTermYears));
        Assert.That(dto.MonthlyPayment, Is.EqualTo(mortgage.MonthlyPayment));
        Assert.That(dto.StartDate, Is.EqualTo(mortgage.StartDate));
        Assert.That(dto.MortgageType, Is.EqualTo(mortgage.MortgageType));
        Assert.That(dto.IsActive, Is.EqualTo(mortgage.IsActive));
        Assert.That(dto.Notes, Is.EqualTo(mortgage.Notes));
    }
}
