// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core.Tests;

public class ApplianceTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesAppliance()
    {
        // Arrange
        var applianceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Samsung Refrigerator";
        var applianceType = ApplianceType.Refrigerator;
        var brand = "Samsung";
        var modelNumber = "RF28R7351SR";
        var serialNumber = "SN123456789";
        var purchaseDate = DateTime.UtcNow.AddMonths(-6);
        var purchasePrice = 2499.99m;

        // Act
        var appliance = new Appliance
        {
            ApplianceId = applianceId,
            UserId = userId,
            Name = name,
            ApplianceType = applianceType,
            Brand = brand,
            ModelNumber = modelNumber,
            SerialNumber = serialNumber,
            PurchaseDate = purchaseDate,
            PurchasePrice = purchasePrice
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(appliance.ApplianceId, Is.EqualTo(applianceId));
            Assert.That(appliance.UserId, Is.EqualTo(userId));
            Assert.That(appliance.Name, Is.EqualTo(name));
            Assert.That(appliance.ApplianceType, Is.EqualTo(applianceType));
            Assert.That(appliance.Brand, Is.EqualTo(brand));
            Assert.That(appliance.ModelNumber, Is.EqualTo(modelNumber));
            Assert.That(appliance.SerialNumber, Is.EqualTo(serialNumber));
            Assert.That(appliance.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(appliance.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(appliance.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var appliance = new Appliance();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(appliance.Name, Is.EqualTo(string.Empty));
            Assert.That(appliance.Brand, Is.Null);
            Assert.That(appliance.ModelNumber, Is.Null);
            Assert.That(appliance.SerialNumber, Is.Null);
            Assert.That(appliance.PurchaseDate, Is.Null);
            Assert.That(appliance.PurchasePrice, Is.Null);
            Assert.That(appliance.Warranties, Is.Not.Null);
            Assert.That(appliance.Warranties, Is.Empty);
            Assert.That(appliance.Manuals, Is.Not.Null);
            Assert.That(appliance.Manuals, Is.Empty);
            Assert.That(appliance.ServiceRecords, Is.Not.Null);
            Assert.That(appliance.ServiceRecords, Is.Empty);
            Assert.That(appliance.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ApplianceType_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var appliance = new Appliance();

        // Act
        appliance.ApplianceType = ApplianceType.Dishwasher;

        // Assert
        Assert.That(appliance.ApplianceType, Is.EqualTo(ApplianceType.Dishwasher));
    }

    [Test]
    public void Name_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var appliance = new Appliance();
        var name = "LG Washing Machine";

        // Act
        appliance.Name = name;

        // Assert
        Assert.That(appliance.Name, Is.EqualTo(name));
    }

    [Test]
    public void Brand_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var appliance = new Appliance();
        var brand = "Whirlpool";

        // Act
        appliance.Brand = brand;

        // Assert
        Assert.That(appliance.Brand, Is.EqualTo(brand));
    }

    [Test]
    public void PurchasePrice_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var appliance = new Appliance();
        var price = 1299.50m;

        // Act
        appliance.PurchasePrice = price;

        // Assert
        Assert.That(appliance.PurchasePrice, Is.EqualTo(price));
    }

    [Test]
    public void Warranties_CanAddWarranty_AddsCorrectly()
    {
        // Arrange
        var appliance = new Appliance();
        var warranty = new Warranty { WarrantyId = Guid.NewGuid() };

        // Act
        appliance.Warranties.Add(warranty);

        // Assert
        Assert.That(appliance.Warranties, Has.Count.EqualTo(1));
        Assert.That(appliance.Warranties, Contains.Item(warranty));
    }

    [Test]
    public void Manuals_CanAddManual_AddsCorrectly()
    {
        // Arrange
        var appliance = new Appliance();
        var manual = new Manual { ManualId = Guid.NewGuid() };

        // Act
        appliance.Manuals.Add(manual);

        // Assert
        Assert.That(appliance.Manuals, Has.Count.EqualTo(1));
        Assert.That(appliance.Manuals, Contains.Item(manual));
    }

    [Test]
    public void ServiceRecords_CanAddServiceRecord_AddsCorrectly()
    {
        // Arrange
        var appliance = new Appliance();
        var serviceRecord = new ServiceRecord { ServiceRecordId = Guid.NewGuid() };

        // Act
        appliance.ServiceRecords.Add(serviceRecord);

        // Assert
        Assert.That(appliance.ServiceRecords, Has.Count.EqualTo(1));
        Assert.That(appliance.ServiceRecords, Contains.Item(serviceRecord));
    }

    [Test]
    public void PurchaseDate_CanBeSetAndCleared_UpdatesCorrectly()
    {
        // Arrange
        var appliance = new Appliance { PurchaseDate = DateTime.UtcNow };

        // Act
        appliance.PurchaseDate = null;

        // Assert
        Assert.That(appliance.PurchaseDate, Is.Null);
    }

    [Test]
    public void ModelNumber_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var appliance = new Appliance { ModelNumber = "ABC123" };

        // Act
        appliance.ModelNumber = null;

        // Assert
        Assert.That(appliance.ModelNumber, Is.Null);
    }

    [Test]
    public void SerialNumber_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var appliance = new Appliance();
        var serialNumber = "SN987654321";

        // Act
        appliance.SerialNumber = serialNumber;

        // Assert
        Assert.That(appliance.SerialNumber, Is.EqualTo(serialNumber));
    }
}
