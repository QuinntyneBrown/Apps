// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core.Tests;

public class DividendReceivedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesDividendReceivedEvent()
    {
        var dividendId = Guid.NewGuid();
        var holdingId = Guid.NewGuid();
        var totalAmount = 250m;
        var paymentDate = new DateTime(2025, 3, 15);
        var timestamp = DateTime.UtcNow;

        var evt = new DividendReceivedEvent
        {
            DividendId = dividendId,
            HoldingId = holdingId,
            TotalAmount = totalAmount,
            PaymentDate = paymentDate,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.DividendId, Is.EqualTo(dividendId));
            Assert.That(evt.HoldingId, Is.EqualTo(holdingId));
            Assert.That(evt.TotalAmount, Is.EqualTo(totalAmount));
            Assert.That(evt.PaymentDate, Is.EqualTo(paymentDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        var evt = new DividendReceivedEvent();
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var id = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;
        var evt1 = new DividendReceivedEvent { DividendId = id, TotalAmount = 100m, PaymentDate = timestamp, Timestamp = timestamp };
        var evt2 = new DividendReceivedEvent { DividendId = id, TotalAmount = 100m, PaymentDate = timestamp, Timestamp = timestamp };
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new DividendReceivedEvent { DividendId = Guid.NewGuid(), TotalAmount = 100m };
        var modified = original with { TotalAmount = 200m };
        Assert.That(modified.TotalAmount, Is.EqualTo(200m));
        Assert.That(modified, Is.Not.SameAs(original));
    }
}
