// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class OfferTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesOffer()
    {
        var offerId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var applicationId = Guid.NewGuid();
        var salary = 120000m;
        var currency = "USD";
        var bonus = 10000m;
        var offerDate = new DateTime(2025, 1, 25);

        var offer = new Offer
        {
            OfferId = offerId,
            UserId = userId,
            ApplicationId = applicationId,
            Salary = salary,
            Currency = currency,
            Bonus = bonus,
            OfferDate = offerDate
        };

        Assert.Multiple(() =>
        {
            Assert.That(offer.OfferId, Is.EqualTo(offerId));
            Assert.That(offer.UserId, Is.EqualTo(userId));
            Assert.That(offer.ApplicationId, Is.EqualTo(applicationId));
            Assert.That(offer.Salary, Is.EqualTo(salary));
            Assert.That(offer.Currency, Is.EqualTo(currency));
            Assert.That(offer.Bonus, Is.EqualTo(bonus));
            Assert.That(offer.OfferDate, Is.EqualTo(offerDate));
            Assert.That(offer.IsAccepted, Is.False);
            Assert.That(offer.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Accept_SetsAcceptedFlagAndTimestamps()
    {
        var offer = new Offer { IsAccepted = false, DecisionDate = null, UpdatedAt = null };

        offer.Accept();

        Assert.Multiple(() =>
        {
            Assert.That(offer.IsAccepted, Is.True);
            Assert.That(offer.DecisionDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(offer.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Decline_SetsFlagToFalseAndTimestamps()
    {
        var offer = new Offer { IsAccepted = true, DecisionDate = null, UpdatedAt = null };

        offer.Decline();

        Assert.Multiple(() =>
        {
            Assert.That(offer.IsAccepted, Is.False);
            Assert.That(offer.DecisionDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(offer.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsExpired_ExpiredWithNoDecision_ReturnsTrue()
    {
        var offer = new Offer
        {
            ExpirationDate = DateTime.UtcNow.AddDays(-1),
            DecisionDate = null
        };

        Assert.That(offer.IsExpired(), Is.True);
    }

    [Test]
    public void IsExpired_NotExpired_ReturnsFalse()
    {
        var offer = new Offer
        {
            ExpirationDate = DateTime.UtcNow.AddDays(7),
            DecisionDate = null
        };

        Assert.That(offer.IsExpired(), Is.False);
    }

    [Test]
    public void IsExpired_ExpiredButDecisionMade_ReturnsFalse()
    {
        var offer = new Offer
        {
            ExpirationDate = DateTime.UtcNow.AddDays(-1),
            DecisionDate = DateTime.UtcNow
        };

        Assert.That(offer.IsExpired(), Is.False);
    }

    [Test]
    public void IsExpired_NoExpirationDate_ReturnsFalse()
    {
        var offer = new Offer
        {
            ExpirationDate = null,
            DecisionDate = null
        };

        Assert.That(offer.IsExpired(), Is.False);
    }

    [Test]
    public void Currency_DefaultValue_IsUSD()
    {
        var offer = new Offer();
        Assert.That(offer.Currency, Is.EqualTo("USD"));
    }

    [Test]
    public void IsAccepted_DefaultValue_IsFalse()
    {
        var offer = new Offer();
        Assert.That(offer.IsAccepted, Is.False);
    }

    [Test]
    public void OptionalFields_CanBeNull()
    {
        var offer = new Offer
        {
            Bonus = null,
            Equity = null,
            Benefits = null,
            VacationDays = null,
            ExpirationDate = null,
            DecisionDate = null,
            Notes = null
        };

        Assert.Multiple(() =>
        {
            Assert.That(offer.Bonus, Is.Null);
            Assert.That(offer.Equity, Is.Null);
            Assert.That(offer.Benefits, Is.Null);
            Assert.That(offer.VacationDays, Is.Null);
            Assert.That(offer.ExpirationDate, Is.Null);
            Assert.That(offer.DecisionDate, Is.Null);
            Assert.That(offer.Notes, Is.Null);
        });
    }
}
