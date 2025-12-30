// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core.Tests;

public class WarrantyTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesWarranty()
    {
        // Arrange
        var warrantyId = Guid.NewGuid();
        var applianceId = Guid.NewGuid();
        var provider = "Samsung Extended Warranty";
        var startDate = DateTime.UtcNow.AddMonths(-6);
        var endDate = DateTime.UtcNow.AddMonths(18);
        var coverageDetails = "Full parts and labor coverage";
        var documentUrl = "https://example.com/warranty.pdf";

        // Act
        var warranty = new Warranty
        {
            WarrantyId = warrantyId,
            ApplianceId = applianceId,
            Provider = provider,
            StartDate = startDate,
            EndDate = endDate,
            CoverageDetails = coverageDetails,
            DocumentUrl = documentUrl
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(warranty.WarrantyId, Is.EqualTo(warrantyId));
            Assert.That(warranty.ApplianceId, Is.EqualTo(applianceId));
            Assert.That(warranty.Provider, Is.EqualTo(provider));
            Assert.That(warranty.StartDate, Is.EqualTo(startDate));
            Assert.That(warranty.EndDate, Is.EqualTo(endDate));
            Assert.That(warranty.CoverageDetails, Is.EqualTo(coverageDetails));
            Assert.That(warranty.DocumentUrl, Is.EqualTo(documentUrl));
            Assert.That(warranty.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var warranty = new Warranty();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(warranty.Provider, Is.Null);
            Assert.That(warranty.StartDate, Is.Null);
            Assert.That(warranty.EndDate, Is.Null);
            Assert.That(warranty.CoverageDetails, Is.Null);
            Assert.That(warranty.DocumentUrl, Is.Null);
            Assert.That(warranty.Appliance, Is.Null);
            Assert.That(warranty.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Provider_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var warranty = new Warranty();
        var provider = "LG Extended Protection";

        // Act
        warranty.Provider = provider;

        // Assert
        Assert.That(warranty.Provider, Is.EqualTo(provider));
    }

    [Test]
    public void StartDate_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var warranty = new Warranty();
        var startDate = DateTime.UtcNow;

        // Act
        warranty.StartDate = startDate;

        // Assert
        Assert.That(warranty.StartDate, Is.EqualTo(startDate));
    }

    [Test]
    public void EndDate_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var warranty = new Warranty();
        var endDate = DateTime.UtcNow.AddYears(2);

        // Act
        warranty.EndDate = endDate;

        // Assert
        Assert.That(warranty.EndDate, Is.EqualTo(endDate));
    }

    [Test]
    public void CoverageDetails_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var warranty = new Warranty();
        var details = "Covers all mechanical failures and parts";

        // Act
        warranty.CoverageDetails = details;

        // Assert
        Assert.That(warranty.CoverageDetails, Is.EqualTo(details));
    }

    [Test]
    public void DocumentUrl_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var warranty = new Warranty();
        var url = "https://example.com/warranty-doc.pdf";

        // Act
        warranty.DocumentUrl = url;

        // Assert
        Assert.That(warranty.DocumentUrl, Is.EqualTo(url));
    }

    [Test]
    public void Appliance_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var warranty = new Warranty();
        var appliance = new Appliance { ApplianceId = Guid.NewGuid() };

        // Act
        warranty.Appliance = appliance;

        // Assert
        Assert.That(warranty.Appliance, Is.EqualTo(appliance));
    }

    [Test]
    public void StartDate_CanBeSetAndCleared_UpdatesCorrectly()
    {
        // Arrange
        var warranty = new Warranty { StartDate = DateTime.UtcNow };

        // Act
        warranty.StartDate = null;

        // Assert
        Assert.That(warranty.StartDate, Is.Null);
    }

    [Test]
    public void EndDate_CanBeSetAndCleared_UpdatesCorrectly()
    {
        // Arrange
        var warranty = new Warranty { EndDate = DateTime.UtcNow.AddYears(1) };

        // Act
        warranty.EndDate = null;

        // Assert
        Assert.That(warranty.EndDate, Is.Null);
    }

    [Test]
    public void ApplianceId_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var warranty = new Warranty();
        var applianceId = Guid.NewGuid();

        // Act
        warranty.ApplianceId = applianceId;

        // Assert
        Assert.That(warranty.ApplianceId, Is.EqualTo(applianceId));
    }
}
