// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class OfferAcceptedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesOfferAcceptedEvent()
    {
        var offerId = Guid.NewGuid();
        var decisionDate = new DateTime(2025, 1, 26);
        var timestamp = DateTime.UtcNow;

        var evt = new OfferAcceptedEvent
        {
            OfferId = offerId,
            DecisionDate = decisionDate,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.OfferId, Is.EqualTo(offerId));
            Assert.That(evt.DecisionDate, Is.EqualTo(decisionDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        var evt = new OfferAcceptedEvent();
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var id = Guid.NewGuid();
        var decisionDate = DateTime.UtcNow;

        var evt1 = new OfferAcceptedEvent { OfferId = id, DecisionDate = decisionDate, Timestamp = decisionDate };
        var evt2 = new OfferAcceptedEvent { OfferId = id, DecisionDate = decisionDate, Timestamp = decisionDate };

        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new OfferAcceptedEvent { OfferId = Guid.NewGuid(), DecisionDate = DateTime.UtcNow };
        var newDate = DateTime.UtcNow.AddDays(1);
        var modified = original with { DecisionDate = newDate };

        Assert.That(modified.DecisionDate, Is.EqualTo(newDate));
        Assert.That(modified, Is.Not.SameAs(original));
    }
}
