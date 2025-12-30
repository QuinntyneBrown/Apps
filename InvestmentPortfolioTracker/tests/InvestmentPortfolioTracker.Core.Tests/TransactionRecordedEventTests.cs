// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core.Tests;

public class TransactionRecordedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTransactionRecordedEvent()
    {
        var transactionId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var transactionType = TransactionType.Buy;
        var amount = 1000m;
        var timestamp = DateTime.UtcNow;

        var evt = new TransactionRecordedEvent
        {
            TransactionId = transactionId,
            AccountId = accountId,
            TransactionType = transactionType,
            Amount = amount,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.TransactionId, Is.EqualTo(transactionId));
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.TransactionType, Is.EqualTo(transactionType));
            Assert.That(evt.Amount, Is.EqualTo(amount));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        var evt = new TransactionRecordedEvent();
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var id = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;
        var evt1 = new TransactionRecordedEvent { TransactionId = id, TransactionType = TransactionType.Buy, Amount = 1000m, Timestamp = timestamp };
        var evt2 = new TransactionRecordedEvent { TransactionId = id, TransactionType = TransactionType.Buy, Amount = 1000m, Timestamp = timestamp };
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new TransactionRecordedEvent { TransactionId = Guid.NewGuid(), Amount = 1000m };
        var modified = original with { Amount = 2000m };
        Assert.That(modified.Amount, Is.EqualTo(2000m));
    }
}
