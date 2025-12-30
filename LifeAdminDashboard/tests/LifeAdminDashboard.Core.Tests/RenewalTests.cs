// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core.Tests;

public class RenewalTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesRenewal()
    {
        // Arrange
        var renewalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Netflix Subscription";
        var renewalType = "Subscription";
        var renewalDate = DateTime.UtcNow.AddMonths(1);
        var frequency = "Monthly";

        // Act
        var renewal = new Renewal
        {
            RenewalId = renewalId,
            UserId = userId,
            Name = name,
            RenewalType = renewalType,
            RenewalDate = renewalDate,
            Frequency = frequency
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(renewal.RenewalId, Is.EqualTo(renewalId));
            Assert.That(renewal.UserId, Is.EqualTo(userId));
            Assert.That(renewal.Name, Is.EqualTo(name));
            Assert.That(renewal.RenewalType, Is.EqualTo(renewalType));
            Assert.That(renewal.RenewalDate, Is.EqualTo(renewalDate));
            Assert.That(renewal.Frequency, Is.EqualTo(frequency));
            Assert.That(renewal.IsActive, Is.True);
            Assert.That(renewal.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void IsDueSoon_WhenActiveAndWithin30Days_ReturnsTrue()
    {
        // Arrange
        var renewal = new Renewal
        {
            IsActive = true,
            RenewalDate = DateTime.UtcNow.AddDays(20)
        };

        // Act
        var result = renewal.IsDueSoon();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsDueSoon_WhenActiveAndBeyond30Days_ReturnsFalse()
    {
        // Arrange
        var renewal = new Renewal
        {
            IsActive = true,
            RenewalDate = DateTime.UtcNow.AddDays(40)
        };

        // Act
        var result = renewal.IsDueSoon();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsDueSoon_WhenNotActive_ReturnsFalse()
    {
        // Arrange
        var renewal = new Renewal
        {
            IsActive = false,
            RenewalDate = DateTime.UtcNow.AddDays(20)
        };

        // Act
        var result = renewal.IsDueSoon();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Cancel_WhenCalled_SetsIsActiveFalse()
    {
        // Arrange
        var renewal = new Renewal { IsActive = true };

        // Act
        renewal.Cancel();

        // Assert
        Assert.That(renewal.IsActive, Is.False);
    }

    [Test]
    public void Renew_WithMonthlyFrequency_AddsOneMonth()
    {
        // Arrange
        var originalDate = DateTime.UtcNow;
        var renewal = new Renewal
        {
            RenewalDate = originalDate,
            Frequency = "Monthly"
        };

        // Act
        renewal.Renew();

        // Assert
        Assert.That(renewal.RenewalDate, Is.EqualTo(originalDate.AddMonths(1)));
    }

    [Test]
    public void Renew_WithYearlyFrequency_AddsOneYear()
    {
        // Arrange
        var originalDate = DateTime.UtcNow;
        var renewal = new Renewal
        {
            RenewalDate = originalDate,
            Frequency = "Yearly"
        };

        // Act
        renewal.Renew();

        // Assert
        Assert.That(renewal.RenewalDate, Is.EqualTo(originalDate.AddYears(1)));
    }

    [Test]
    public void Renew_WithMonthFrequencyLowerCase_AddsOneMonth()
    {
        // Arrange
        var originalDate = DateTime.UtcNow;
        var renewal = new Renewal
        {
            RenewalDate = originalDate,
            Frequency = "month"
        };

        // Act
        renewal.Renew();

        // Assert
        Assert.That(renewal.RenewalDate, Is.EqualTo(originalDate.AddMonths(1)));
    }

    [Test]
    public void Renew_WithYearFrequencyLowerCase_AddsOneYear()
    {
        // Arrange
        var originalDate = DateTime.UtcNow;
        var renewal = new Renewal
        {
            RenewalDate = originalDate,
            Frequency = "year"
        };

        // Act
        renewal.Renew();

        // Assert
        Assert.That(renewal.RenewalDate, Is.EqualTo(originalDate.AddYears(1)));
    }

    [Test]
    public void IsActive_DefaultsToTrue()
    {
        // Arrange & Act
        var renewal = new Renewal();

        // Assert
        Assert.That(renewal.IsActive, Is.True);
    }

    [Test]
    public void IsAutoRenewal_DefaultsToFalse()
    {
        // Arrange & Act
        var renewal = new Renewal();

        // Assert
        Assert.That(renewal.IsAutoRenewal, Is.False);
    }

    [Test]
    public void Cost_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var renewal = new Renewal();
        var expectedCost = 15.99m;

        // Act
        renewal.Cost = expectedCost;

        // Assert
        Assert.That(renewal.Cost, Is.EqualTo(expectedCost));
    }

    [Test]
    public void Provider_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var renewal = new Renewal();
        var expectedProvider = "Netflix";

        // Act
        renewal.Provider = expectedProvider;

        // Assert
        Assert.That(renewal.Provider, Is.EqualTo(expectedProvider));
    }
}
