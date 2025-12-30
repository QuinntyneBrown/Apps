// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class OfferReceivedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesOfferReceivedEvent()
    {
        var offerId = Guid.NewGuid();
        var applicationId = Guid.NewGuid();
        var salary = 120000m;
        var offerDate = new DateTime(2025, 1, 25);
        var timestamp = DateTime.UtcNow;

        var evt = new OfferReceivedEvent
        {
            OfferId = offerId,
            ApplicationId = applicationId,
            Salary = salary,
            OfferDate = offerDate,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.OfferId, Is.EqualTo(offerId));
            Assert.That(evt.ApplicationId, Is.EqualTo(applicationId));
            Assert.That(evt.Salary, Is.EqualTo(salary));
            Assert.That(evt.OfferDate, Is.EqualTo(offerDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var id = Guid.NewGuid();
        var offerDate = DateTime.UtcNow;

        var evt1 = new OfferReceivedEvent { OfferId = id, Salary = 100000m, OfferDate = offerDate, Timestamp = offerDate };
        var evt2 = new OfferReceivedEvent { OfferId = id, Salary = 100000m, OfferDate = offerDate, Timestamp = offerDate };

        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new OfferReceivedEvent { OfferId = Guid.NewGuid(), Salary = 100000m };
        var modified = original with { Salary = 120000m };

        Assert.That(modified.Salary, Is.EqualTo(120000m));
    }
}
