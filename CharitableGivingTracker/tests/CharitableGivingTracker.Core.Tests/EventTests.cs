// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Core.Tests;

public class EventTests
{
    [Test]
    public void DonationRecordedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var donationId = Guid.NewGuid();
        var organizationId = Guid.NewGuid();
        var beforeCreate = DateTime.UtcNow;

        // Act
        var evt = new DonationRecordedEvent
        {
            DonationId = donationId,
            OrganizationId = organizationId,
            Amount = 250.00m
        };
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DonationId, Is.EqualTo(donationId));
            Assert.That(evt.OrganizationId, Is.EqualTo(organizationId));
            Assert.That(evt.Amount, Is.EqualTo(250.00m));
            Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void DonationRecordedEvent_WithLargeAmount_StoresAmountCorrectly()
    {
        // Arrange
        var donationId = Guid.NewGuid();
        var organizationId = Guid.NewGuid();

        // Act
        var evt = new DonationRecordedEvent
        {
            DonationId = donationId,
            OrganizationId = organizationId,
            Amount = 10000.00m
        };

        // Assert
        Assert.That(evt.Amount, Is.EqualTo(10000.00m));
    }

    [Test]
    public void DonationRecordedEvent_WithSmallAmount_StoresAmountCorrectly()
    {
        // Arrange
        var donationId = Guid.NewGuid();
        var organizationId = Guid.NewGuid();

        // Act
        var evt = new DonationRecordedEvent
        {
            DonationId = donationId,
            OrganizationId = organizationId,
            Amount = 5.00m
        };

        // Assert
        Assert.That(evt.Amount, Is.EqualTo(5.00m));
    }

    [Test]
    public void OrganizationAddedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        var beforeCreate = DateTime.UtcNow;

        // Act
        var evt = new OrganizationAddedEvent
        {
            OrganizationId = organizationId,
            Name = "Habitat for Humanity"
        };
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.OrganizationId, Is.EqualTo(organizationId));
            Assert.That(evt.Name, Is.EqualTo("Habitat for Humanity"));
            Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void OrganizationAddedEvent_WithLongName_StoresNameCorrectly()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        // Act
        var evt = new OrganizationAddedEvent
        {
            OrganizationId = organizationId,
            Name = "The American Society for the Prevention of Cruelty to Animals"
        };

        // Assert
        Assert.That(evt.Name, Is.EqualTo("The American Society for the Prevention of Cruelty to Animals"));
    }

    [Test]
    public void TaxReportGeneratedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var beforeCreate = DateTime.UtcNow;

        // Act
        var evt = new TaxReportGeneratedEvent
        {
            TaxReportId = reportId,
            TaxYear = 2024,
            TotalDeductibleAmount = 5000.00m
        };
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TaxReportId, Is.EqualTo(reportId));
            Assert.That(evt.TaxYear, Is.EqualTo(2024));
            Assert.That(evt.TotalDeductibleAmount, Is.EqualTo(5000.00m));
            Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void TaxReportGeneratedEvent_WithPastYear_StoresYearCorrectly()
    {
        // Arrange
        var reportId = Guid.NewGuid();

        // Act
        var evt = new TaxReportGeneratedEvent
        {
            TaxReportId = reportId,
            TaxYear = 2023,
            TotalDeductibleAmount = 3500.00m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TaxYear, Is.EqualTo(2023));
            Assert.That(evt.TotalDeductibleAmount, Is.EqualTo(3500.00m));
        });
    }

    [Test]
    public void TaxReportGeneratedEvent_WithLargeDeductibleAmount_StoresAmountCorrectly()
    {
        // Arrange
        var reportId = Guid.NewGuid();

        // Act
        var evt = new TaxReportGeneratedEvent
        {
            TaxReportId = reportId,
            TaxYear = 2024,
            TotalDeductibleAmount = 100000.00m
        };

        // Assert
        Assert.That(evt.TotalDeductibleAmount, Is.EqualTo(100000.00m));
    }

    [Test]
    public void TaxReportGeneratedEvent_WithZeroDeductions_StoresZero()
    {
        // Arrange
        var reportId = Guid.NewGuid();

        // Act
        var evt = new TaxReportGeneratedEvent
        {
            TaxReportId = reportId,
            TaxYear = 2024,
            TotalDeductibleAmount = 0m
        };

        // Assert
        Assert.That(evt.TotalDeductibleAmount, Is.EqualTo(0m));
    }
}
