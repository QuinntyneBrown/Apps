// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core.Tests;

public class ServiceRecordTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesServiceRecord()
    {
        // Arrange
        var serviceRecordId = Guid.NewGuid();
        var applianceId = Guid.NewGuid();
        var serviceDate = DateTime.UtcNow.AddMonths(-2);
        var serviceProvider = "ABC Appliance Repair";
        var description = "Replaced compressor and recharged refrigerant";
        var cost = 450.75m;

        // Act
        var serviceRecord = new ServiceRecord
        {
            ServiceRecordId = serviceRecordId,
            ApplianceId = applianceId,
            ServiceDate = serviceDate,
            ServiceProvider = serviceProvider,
            Description = description,
            Cost = cost
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(serviceRecord.ServiceRecordId, Is.EqualTo(serviceRecordId));
            Assert.That(serviceRecord.ApplianceId, Is.EqualTo(applianceId));
            Assert.That(serviceRecord.ServiceDate, Is.EqualTo(serviceDate));
            Assert.That(serviceRecord.ServiceProvider, Is.EqualTo(serviceProvider));
            Assert.That(serviceRecord.Description, Is.EqualTo(description));
            Assert.That(serviceRecord.Cost, Is.EqualTo(cost));
            Assert.That(serviceRecord.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var serviceRecord = new ServiceRecord();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(serviceRecord.ServiceProvider, Is.Null);
            Assert.That(serviceRecord.Description, Is.Null);
            Assert.That(serviceRecord.Cost, Is.Null);
            Assert.That(serviceRecord.Appliance, Is.Null);
            Assert.That(serviceRecord.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ServiceDate_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var serviceRecord = new ServiceRecord();
        var serviceDate = DateTime.UtcNow.AddDays(-7);

        // Act
        serviceRecord.ServiceDate = serviceDate;

        // Assert
        Assert.That(serviceRecord.ServiceDate, Is.EqualTo(serviceDate));
    }

    [Test]
    public void ServiceProvider_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var serviceRecord = new ServiceRecord();
        var provider = "XYZ Repair Services";

        // Act
        serviceRecord.ServiceProvider = provider;

        // Assert
        Assert.That(serviceRecord.ServiceProvider, Is.EqualTo(provider));
    }

    [Test]
    public void Description_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var serviceRecord = new ServiceRecord();
        var description = "Annual maintenance check";

        // Act
        serviceRecord.Description = description;

        // Assert
        Assert.That(serviceRecord.Description, Is.EqualTo(description));
    }

    [Test]
    public void Cost_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var serviceRecord = new ServiceRecord();
        var cost = 125.50m;

        // Act
        serviceRecord.Cost = cost;

        // Assert
        Assert.That(serviceRecord.Cost, Is.EqualTo(cost));
    }

    [Test]
    public void Cost_CanBeSetAndCleared_UpdatesCorrectly()
    {
        // Arrange
        var serviceRecord = new ServiceRecord { Cost = 200.00m };

        // Act
        serviceRecord.Cost = null;

        // Assert
        Assert.That(serviceRecord.Cost, Is.Null);
    }

    [Test]
    public void Appliance_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var serviceRecord = new ServiceRecord();
        var appliance = new Appliance { ApplianceId = Guid.NewGuid() };

        // Act
        serviceRecord.Appliance = appliance;

        // Assert
        Assert.That(serviceRecord.Appliance, Is.EqualTo(appliance));
    }

    [Test]
    public void ApplianceId_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var serviceRecord = new ServiceRecord();
        var applianceId = Guid.NewGuid();

        // Act
        serviceRecord.ApplianceId = applianceId;

        // Assert
        Assert.That(serviceRecord.ApplianceId, Is.EqualTo(applianceId));
    }

    [Test]
    public void ServiceProvider_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var serviceRecord = new ServiceRecord { ServiceProvider = "Some Provider" };

        // Act
        serviceRecord.ServiceProvider = null;

        // Assert
        Assert.That(serviceRecord.ServiceProvider, Is.Null);
    }

    [Test]
    public void Description_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var serviceRecord = new ServiceRecord { Description = "Some description" };

        // Act
        serviceRecord.Description = null;

        // Assert
        Assert.That(serviceRecord.Description, Is.Null);
    }
}
