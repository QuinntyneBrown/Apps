// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core.Tests;

public class EventsTests
{
    [Test]
    public void ApplianceAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var applianceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Samsung Refrigerator";

        // Act
        var evt = new ApplianceAddedEvent
        {
            ApplianceId = applianceId,
            UserId = userId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ApplianceId, Is.EqualTo(applianceId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ApplianceAddedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var applianceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "LG Dishwasher";
        var timestamp = DateTime.UtcNow.AddMinutes(-5);

        // Act
        var evt = new ApplianceAddedEvent
        {
            ApplianceId = applianceId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void ApplianceAddedEvent_EmptyName_CreatesEvent()
    {
        // Arrange
        var applianceId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new ApplianceAddedEvent
        {
            ApplianceId = applianceId,
            UserId = userId,
            Name = string.Empty
        };

        // Assert
        Assert.That(evt.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void WarrantyAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var warrantyId = Guid.NewGuid();
        var applianceId = Guid.NewGuid();

        // Act
        var evt = new WarrantyAddedEvent
        {
            WarrantyId = warrantyId,
            ApplianceId = applianceId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.WarrantyId, Is.EqualTo(warrantyId));
            Assert.That(evt.ApplianceId, Is.EqualTo(applianceId));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void WarrantyAddedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var warrantyId = Guid.NewGuid();
        var applianceId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow.AddMinutes(-10);

        // Act
        var evt = new WarrantyAddedEvent
        {
            WarrantyId = warrantyId,
            ApplianceId = applianceId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void ServiceRecordAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var serviceRecordId = Guid.NewGuid();
        var applianceId = Guid.NewGuid();
        var serviceDate = DateTime.UtcNow.AddDays(-7);

        // Act
        var evt = new ServiceRecordAddedEvent
        {
            ServiceRecordId = serviceRecordId,
            ApplianceId = applianceId,
            ServiceDate = serviceDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ServiceRecordId, Is.EqualTo(serviceRecordId));
            Assert.That(evt.ApplianceId, Is.EqualTo(applianceId));
            Assert.That(evt.ServiceDate, Is.EqualTo(serviceDate));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ServiceRecordAddedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var serviceRecordId = Guid.NewGuid();
        var applianceId = Guid.NewGuid();
        var serviceDate = DateTime.UtcNow.AddDays(-3);
        var timestamp = DateTime.UtcNow.AddMinutes(-2);

        // Act
        var evt = new ServiceRecordAddedEvent
        {
            ServiceRecordId = serviceRecordId,
            ApplianceId = applianceId,
            ServiceDate = serviceDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void ManualUploadedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var manualId = Guid.NewGuid();
        var applianceId = Guid.NewGuid();

        // Act
        var evt = new ManualUploadedEvent
        {
            ManualId = manualId,
            ApplianceId = applianceId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ManualId, Is.EqualTo(manualId));
            Assert.That(evt.ApplianceId, Is.EqualTo(applianceId));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ManualUploadedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var manualId = Guid.NewGuid();
        var applianceId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow.AddMinutes(-15);

        // Act
        var evt = new ManualUploadedEvent
        {
            ManualId = manualId,
            ApplianceId = applianceId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void ApplianceAddedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new ApplianceAddedEvent
        {
            ApplianceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Old Name"
        };
        var newName = "New Name";

        // Act
        var newEvt = evt with { Name = newName };

        // Assert
        Assert.That(newEvt.Name, Is.EqualTo(newName));
        Assert.That(newEvt.ApplianceId, Is.EqualTo(evt.ApplianceId));
    }

    [Test]
    public void WarrantyAddedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new WarrantyAddedEvent
        {
            WarrantyId = Guid.NewGuid(),
            ApplianceId = Guid.NewGuid()
        };
        var newWarrantyId = Guid.NewGuid();

        // Act
        var newEvt = evt with { WarrantyId = newWarrantyId };

        // Assert
        Assert.That(newEvt.WarrantyId, Is.EqualTo(newWarrantyId));
        Assert.That(newEvt.ApplianceId, Is.EqualTo(evt.ApplianceId));
    }

    [Test]
    public void ServiceRecordAddedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new ServiceRecordAddedEvent
        {
            ServiceRecordId = Guid.NewGuid(),
            ApplianceId = Guid.NewGuid(),
            ServiceDate = DateTime.UtcNow
        };
        var newServiceDate = DateTime.UtcNow.AddDays(-10);

        // Act
        var newEvt = evt with { ServiceDate = newServiceDate };

        // Assert
        Assert.That(newEvt.ServiceDate, Is.EqualTo(newServiceDate));
        Assert.That(newEvt.ServiceRecordId, Is.EqualTo(evt.ServiceRecordId));
    }

    [Test]
    public void ManualUploadedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new ManualUploadedEvent
        {
            ManualId = Guid.NewGuid(),
            ApplianceId = Guid.NewGuid()
        };
        var newManualId = Guid.NewGuid();

        // Act
        var newEvt = evt with { ManualId = newManualId };

        // Assert
        Assert.That(newEvt.ManualId, Is.EqualTo(newManualId));
        Assert.That(newEvt.ApplianceId, Is.EqualTo(evt.ApplianceId));
    }
}
