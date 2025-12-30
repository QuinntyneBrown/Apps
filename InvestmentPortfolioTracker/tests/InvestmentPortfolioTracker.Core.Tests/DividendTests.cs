// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core.Tests;

public class DividendTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesDividend()
    {
        // Arrange
        var dividendId = Guid.NewGuid();
        var holdingId = Guid.NewGuid();
        var paymentDate = new DateTime(2025, 3, 15);
        var exDividendDate = new DateTime(2025, 3, 1);
        var amountPerShare = 2.5m;
        var totalAmount = 250m;
        var isReinvested = true;

        // Act
        var dividend = new Dividend
        {
            DividendId = dividendId,
            HoldingId = holdingId,
            PaymentDate = paymentDate,
            ExDividendDate = exDividendDate,
            AmountPerShare = amountPerShare,
            TotalAmount = totalAmount,
            IsReinvested = isReinvested
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dividend.DividendId, Is.EqualTo(dividendId));
            Assert.That(dividend.HoldingId, Is.EqualTo(holdingId));
            Assert.That(dividend.PaymentDate, Is.EqualTo(paymentDate));
            Assert.That(dividend.ExDividendDate, Is.EqualTo(exDividendDate));
            Assert.That(dividend.AmountPerShare, Is.EqualTo(amountPerShare));
            Assert.That(dividend.TotalAmount, Is.EqualTo(totalAmount));
            Assert.That(dividend.IsReinvested, Is.EqualTo(isReinvested));
        });
    }

    [Test]
    public void CalculateYield_QuarterlyDividend_ReturnsCorrectYield()
    {
        // Arrange
        var dividend = new Dividend { AmountPerShare = 1m };
        var currentPrice = 100m;

        // Act
        var result = dividend.CalculateYield(currentPrice, 4);

        // Assert
        Assert.That(result, Is.EqualTo(4m));
    }

    [Test]
    public void CalculateYield_AnnualDividend_ReturnsCorrectYield()
    {
        // Arrange
        var dividend = new Dividend { AmountPerShare = 2m };
        var currentPrice = 50m;

        // Act
        var result = dividend.CalculateYield(currentPrice, 1);

        // Assert
        Assert.That(result, Is.EqualTo(4m));
    }

    [Test]
    public void CalculateYield_ZeroPrice_ReturnsZero()
    {
        // Arrange
        var dividend = new Dividend { AmountPerShare = 1m };

        // Act
        var result = dividend.CalculateYield(0m, 4);

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateYield_NegativePrice_ReturnsZero()
    {
        // Arrange
        var dividend = new Dividend { AmountPerShare = 1m };

        // Act
        var result = dividend.CalculateYield(-10m, 4);

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateYield_DefaultPaymentsPerYear_UsesQuarterly()
    {
        // Arrange
        var dividend = new Dividend { AmountPerShare = 1m };

        // Act
        var result = dividend.CalculateYield(100m);

        // Assert
        Assert.That(result, Is.EqualTo(4m));
    }

    [Test]
    public void IsReinvested_DefaultValue_IsFalse()
    {
        var dividend = new Dividend();
        Assert.That(dividend.IsReinvested, Is.False);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        var dividend = new Dividend { Notes = null };
        Assert.That(dividend.Notes, Is.Null);
    }

    [Test]
    public void Holding_NavigationProperty_CanBeSet()
    {
        var holding = new Holding { HoldingId = Guid.NewGuid() };
        var dividend = new Dividend();
        dividend.Holding = holding;
        Assert.That(dividend.Holding, Is.EqualTo(holding));
    }
}
