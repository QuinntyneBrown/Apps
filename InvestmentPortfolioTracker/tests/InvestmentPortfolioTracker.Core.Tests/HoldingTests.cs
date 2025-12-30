// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core.Tests;

public class HoldingTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesHolding()
    {
        // Arrange
        var holdingId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var symbol = "AAPL";
        var name = "Apple Inc.";
        var shares = 100m;
        var averageCost = 150m;
        var currentPrice = 175m;

        // Act
        var holding = new Holding
        {
            HoldingId = holdingId,
            AccountId = accountId,
            Symbol = symbol,
            Name = name,
            Shares = shares,
            AverageCost = averageCost,
            CurrentPrice = currentPrice
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(holding.HoldingId, Is.EqualTo(holdingId));
            Assert.That(holding.AccountId, Is.EqualTo(accountId));
            Assert.That(holding.Symbol, Is.EqualTo(symbol));
            Assert.That(holding.Name, Is.EqualTo(name));
            Assert.That(holding.Shares, Is.EqualTo(shares));
            Assert.That(holding.AverageCost, Is.EqualTo(averageCost));
            Assert.That(holding.CurrentPrice, Is.EqualTo(currentPrice));
            Assert.That(holding.LastPriceUpdate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void CalculateMarketValue_ValidHolding_ReturnsCorrectValue()
    {
        // Arrange
        var holding = new Holding { Shares = 100m, CurrentPrice = 50m };

        // Act
        var result = holding.CalculateMarketValue();

        // Assert
        Assert.That(result, Is.EqualTo(5000m));
    }

    [Test]
    public void CalculateCostBasis_ValidHolding_ReturnsCorrectValue()
    {
        // Arrange
        var holding = new Holding { Shares = 100m, AverageCost = 40m };

        // Act
        var result = holding.CalculateCostBasis();

        // Assert
        Assert.That(result, Is.EqualTo(4000m));
    }

    [Test]
    public void CalculateUnrealizedGainLoss_ProfitableHolding_ReturnsGain()
    {
        // Arrange
        var holding = new Holding { Shares = 100m, AverageCost = 40m, CurrentPrice = 50m };

        // Act
        var result = holding.CalculateUnrealizedGainLoss();

        // Assert
        Assert.That(result, Is.EqualTo(1000m));
    }

    [Test]
    public void CalculateUnrealizedGainLoss_LosingHolding_ReturnsLoss()
    {
        // Arrange
        var holding = new Holding { Shares = 100m, AverageCost = 50m, CurrentPrice = 40m };

        // Act
        var result = holding.CalculateUnrealizedGainLoss();

        // Assert
        Assert.That(result, Is.EqualTo(-1000m));
    }

    [Test]
    public void UpdatePrice_ValidPrice_UpdatesPriceAndTimestamp()
    {
        // Arrange
        var holding = new Holding { CurrentPrice = 50m };
        var beforeUpdate = DateTime.UtcNow;

        // Act
        holding.UpdatePrice(60m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(holding.CurrentPrice, Is.EqualTo(60m));
            Assert.That(holding.LastPriceUpdate, Is.GreaterThanOrEqualTo(beforeUpdate));
        });
    }

    [Test]
    public void AddShares_ValidShares_UpdatesSharesAndAverageCost()
    {
        // Arrange
        var holding = new Holding { Shares = 100m, AverageCost = 50m };

        // Act
        holding.AddShares(50m, 60m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(holding.Shares, Is.EqualTo(150m));
            Assert.That(holding.AverageCost, Is.EqualTo(53.333333m).Within(0.001m));
        });
    }

    [Test]
    public void AddShares_FirstPurchase_SetsCorrectAverageCost()
    {
        // Arrange
        var holding = new Holding { Shares = 0m, AverageCost = 0m };

        // Act
        holding.AddShares(100m, 50m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(holding.Shares, Is.EqualTo(100m));
            Assert.That(holding.AverageCost, Is.EqualTo(50m));
        });
    }

    [Test]
    public void RemoveShares_PartialRemoval_UpdatesShares()
    {
        // Arrange
        var holding = new Holding { Shares = 100m, AverageCost = 50m };

        // Act
        holding.RemoveShares(30m);

        // Assert
        Assert.That(holding.Shares, Is.EqualTo(70m));
    }

    [Test]
    public void RemoveShares_RemoveAll_SetsSharesToZero()
    {
        // Arrange
        var holding = new Holding { Shares = 100m };

        // Act
        holding.RemoveShares(100m);

        // Assert
        Assert.That(holding.Shares, Is.EqualTo(0m));
    }

    [Test]
    public void RemoveShares_RemoveMoreThanOwned_ClampsToZero()
    {
        // Arrange
        var holding = new Holding { Shares = 100m };

        // Act
        holding.RemoveShares(150m);

        // Assert
        Assert.That(holding.Shares, Is.EqualTo(0m));
    }

    [Test]
    public void Account_NavigationProperty_CanBeSet()
    {
        var account = new Account { AccountId = Guid.NewGuid() };
        var holding = new Holding();
        holding.Account = account;
        Assert.That(holding.Account, Is.EqualTo(account));
    }
}
