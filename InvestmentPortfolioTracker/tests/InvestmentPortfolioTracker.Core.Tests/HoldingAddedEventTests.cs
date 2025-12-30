// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core.Tests;

public class HoldingAddedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesHoldingAddedEvent()
    {
        var holdingId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var symbol = "AAPL";
        var shares = 100m;
        var timestamp = DateTime.UtcNow;

        var evt = new HoldingAddedEvent
        {
            HoldingId = holdingId,
            AccountId = accountId,
            Symbol = symbol,
            Shares = shares,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.HoldingId, Is.EqualTo(holdingId));
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.Symbol, Is.EqualTo(symbol));
            Assert.That(evt.Shares, Is.EqualTo(shares));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        var evt = new HoldingAddedEvent();
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var holdingId = Guid.NewGuid();
        var evt1 = new HoldingAddedEvent { HoldingId = holdingId, Symbol = "AAPL", Shares = 100m, Timestamp = DateTime.UtcNow };
        var evt2 = new HoldingAddedEvent { HoldingId = holdingId, Symbol = "AAPL", Shares = 100m, Timestamp = evt1.Timestamp };
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new HoldingAddedEvent { HoldingId = Guid.NewGuid(), Symbol = "AAPL", Shares = 100m };
        var modified = original with { Shares = 150m };
        Assert.That(modified.Shares, Is.EqualTo(150m));
        Assert.That(modified, Is.Not.SameAs(original));
    }
}
