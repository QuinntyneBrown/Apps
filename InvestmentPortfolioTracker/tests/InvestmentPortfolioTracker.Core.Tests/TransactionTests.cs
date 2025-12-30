// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core.Tests;

public class TransactionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTransaction()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var holdingId = Guid.NewGuid();
        var transactionDate = new DateTime(2025, 1, 15);
        var transactionType = TransactionType.Buy;
        var symbol = "AAPL";
        var shares = 10m;
        var pricePerShare = 150m;
        var amount = 1500m;
        var fees = 10m;

        // Act
        var transaction = new Transaction
        {
            TransactionId = transactionId,
            AccountId = accountId,
            HoldingId = holdingId,
            TransactionDate = transactionDate,
            TransactionType = transactionType,
            Symbol = symbol,
            Shares = shares,
            PricePerShare = pricePerShare,
            Amount = amount,
            Fees = fees
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(transaction.TransactionId, Is.EqualTo(transactionId));
            Assert.That(transaction.AccountId, Is.EqualTo(accountId));
            Assert.That(transaction.HoldingId, Is.EqualTo(holdingId));
            Assert.That(transaction.TransactionDate, Is.EqualTo(transactionDate));
            Assert.That(transaction.TransactionType, Is.EqualTo(transactionType));
            Assert.That(transaction.Symbol, Is.EqualTo(symbol));
            Assert.That(transaction.Shares, Is.EqualTo(shares));
            Assert.That(transaction.PricePerShare, Is.EqualTo(pricePerShare));
            Assert.That(transaction.Amount, Is.EqualTo(amount));
            Assert.That(transaction.Fees, Is.EqualTo(fees));
        });
    }

    [Test]
    public void CalculateTotalCost_WithFees_ReturnsTotalWithFees()
    {
        // Arrange
        var transaction = new Transaction { Amount = 1000m, Fees = 10m };

        // Act
        var result = transaction.CalculateTotalCost();

        // Assert
        Assert.That(result, Is.EqualTo(1010m));
    }

    [Test]
    public void CalculateTotalCost_NoFees_ReturnsAmount()
    {
        // Arrange
        var transaction = new Transaction { Amount = 1000m, Fees = null };

        // Act
        var result = transaction.CalculateTotalCost();

        // Assert
        Assert.That(result, Is.EqualTo(1000m));
    }

    [Test]
    public void CalculateTotalCost_ZeroFees_ReturnsAmount()
    {
        // Arrange
        var transaction = new Transaction { Amount = 1000m, Fees = 0m };

        // Act
        var result = transaction.CalculateTotalCost();

        // Assert
        Assert.That(result, Is.EqualTo(1000m));
    }

    [Test]
    public void TransactionType_AllTypes_CanBeAssigned()
    {
        var transaction = new Transaction();
        Assert.DoesNotThrow(() => transaction.TransactionType = TransactionType.Buy);
        Assert.DoesNotThrow(() => transaction.TransactionType = TransactionType.Sell);
        Assert.DoesNotThrow(() => transaction.TransactionType = TransactionType.Dividend);
        Assert.DoesNotThrow(() => transaction.TransactionType = TransactionType.Interest);
        Assert.DoesNotThrow(() => transaction.TransactionType = TransactionType.Deposit);
        Assert.DoesNotThrow(() => transaction.TransactionType = TransactionType.Withdrawal);
        Assert.DoesNotThrow(() => transaction.TransactionType = TransactionType.Transfer);
        Assert.DoesNotThrow(() => transaction.TransactionType = TransactionType.Fee);
    }

    [Test]
    public void HoldingId_OptionalField_CanBeNull()
    {
        var transaction = new Transaction { HoldingId = null };
        Assert.That(transaction.HoldingId, Is.Null);
    }

    [Test]
    public void Symbol_OptionalField_CanBeNull()
    {
        var transaction = new Transaction { Symbol = null };
        Assert.That(transaction.Symbol, Is.Null);
    }

    [Test]
    public void Shares_OptionalField_CanBeNull()
    {
        var transaction = new Transaction { Shares = null };
        Assert.That(transaction.Shares, Is.Null);
    }

    [Test]
    public void NavigationProperties_CanBeSet()
    {
        var account = new Account { AccountId = Guid.NewGuid() };
        var holding = new Holding { HoldingId = Guid.NewGuid() };
        var transaction = new Transaction();

        transaction.Account = account;
        transaction.Holding = holding;

        Assert.Multiple(() =>
        {
            Assert.That(transaction.Account, Is.EqualTo(account));
            Assert.That(transaction.Holding, Is.EqualTo(holding));
        });
    }
}
