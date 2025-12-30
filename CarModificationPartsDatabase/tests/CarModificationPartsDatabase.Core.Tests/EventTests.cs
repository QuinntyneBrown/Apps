// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core.Tests;

public class EventTests
{
    [Test]
    public void InstallationStartedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var installationId = Guid.NewGuid();
        var modificationId = Guid.NewGuid();
        var installationDate = new DateTime(2024, 6, 15);
        var beforeCreate = DateTime.UtcNow;

        // Act
        var evt = new InstallationStartedEvent
        {
            InstallationId = installationId,
            ModificationId = modificationId,
            VehicleInfo = "2019 Toyota Supra",
            InstallationDate = installationDate
        };
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.InstallationId, Is.EqualTo(installationId));
            Assert.That(evt.ModificationId, Is.EqualTo(modificationId));
            Assert.That(evt.VehicleInfo, Is.EqualTo("2019 Toyota Supra"));
            Assert.That(evt.InstallationDate, Is.EqualTo(installationDate));
            Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void InstallationCompletedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var installationId = Guid.NewGuid();
        var modificationId = Guid.NewGuid();

        // Act
        var evt = new InstallationCompletedEvent
        {
            InstallationId = installationId,
            ModificationId = modificationId,
            TotalCost = 2500.50m,
            SatisfactionRating = 5
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.InstallationId, Is.EqualTo(installationId));
            Assert.That(evt.ModificationId, Is.EqualTo(modificationId));
            Assert.That(evt.TotalCost, Is.EqualTo(2500.50m));
            Assert.That(evt.SatisfactionRating, Is.EqualTo(5));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void InstallationCompletedEvent_WithNullSatisfactionRating_AllowsNull()
    {
        // Arrange
        var installationId = Guid.NewGuid();
        var modificationId = Guid.NewGuid();

        // Act
        var evt = new InstallationCompletedEvent
        {
            InstallationId = installationId,
            ModificationId = modificationId,
            TotalCost = 1500m,
            SatisfactionRating = null
        };

        // Assert
        Assert.That(evt.SatisfactionRating, Is.Null);
    }

    [Test]
    public void ModificationCreatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var modificationId = Guid.NewGuid();
        var beforeCreate = DateTime.UtcNow;

        // Act
        var evt = new ModificationCreatedEvent
        {
            ModificationId = modificationId,
            Name = "Turbo Kit",
            Category = ModCategory.ForcedInduction
        };
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ModificationId, Is.EqualTo(modificationId));
            Assert.That(evt.Name, Is.EqualTo("Turbo Kit"));
            Assert.That(evt.Category, Is.EqualTo(ModCategory.ForcedInduction));
            Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void PartAddedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var partId = Guid.NewGuid();

        // Act
        var evt = new PartAddedEvent
        {
            PartId = partId,
            Name = "Brembo Brake Pads",
            Manufacturer = "Brembo",
            PartNumber = "P85020N"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PartId, Is.EqualTo(partId));
            Assert.That(evt.Name, Is.EqualTo("Brembo Brake Pads"));
            Assert.That(evt.Manufacturer, Is.EqualTo("Brembo"));
            Assert.That(evt.PartNumber, Is.EqualTo("P85020N"));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void PartAddedEvent_WithNullPartNumber_AllowsNull()
    {
        // Arrange
        var partId = Guid.NewGuid();

        // Act
        var evt = new PartAddedEvent
        {
            PartId = partId,
            Name = "Generic Oil Filter",
            Manufacturer = "Generic",
            PartNumber = null
        };

        // Assert
        Assert.That(evt.PartNumber, Is.Null);
    }

    [Test]
    public void PartPriceUpdatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var partId = Guid.NewGuid();

        // Act
        var evt = new PartPriceUpdatedEvent
        {
            PartId = partId,
            OldPrice = 99.99m,
            NewPrice = 89.99m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PartId, Is.EqualTo(partId));
            Assert.That(evt.OldPrice, Is.EqualTo(99.99m));
            Assert.That(evt.NewPrice, Is.EqualTo(89.99m));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void PartPriceUpdatedEvent_WithPriceIncrease_StoresCorrectly()
    {
        // Arrange
        var partId = Guid.NewGuid();

        // Act
        var evt = new PartPriceUpdatedEvent
        {
            PartId = partId,
            OldPrice = 50m,
            NewPrice = 75m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.OldPrice, Is.EqualTo(50m));
            Assert.That(evt.NewPrice, Is.EqualTo(75m));
        });
    }
}
