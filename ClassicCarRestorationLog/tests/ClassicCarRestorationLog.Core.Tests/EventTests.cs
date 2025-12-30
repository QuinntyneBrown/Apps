// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core.Tests;

public class EventTests
{
    [Test]
    public void ProjectStartedEvent_Properties_CanBeSet()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ProjectStartedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            CarMake = "Ford",
            CarModel = "Mustang",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ProjectId, Is.EqualTo(projectId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.CarMake, Is.EqualTo("Ford"));
            Assert.That(evt.CarModel, Is.EqualTo("Mustang"));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ProjectPhaseChangedEvent_Properties_CanBeSet()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ProjectPhaseChangedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            NewPhase = ProjectPhase.Painting,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ProjectId, Is.EqualTo(projectId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.NewPhase, Is.EqualTo(ProjectPhase.Painting));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void WorkLoggedEvent_Properties_CanBeSet()
    {
        // Arrange
        var workLogId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new WorkLoggedEvent
        {
            WorkLogId = workLogId,
            UserId = userId,
            ProjectId = projectId,
            HoursWorked = 8,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.WorkLogId, Is.EqualTo(workLogId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.ProjectId, Is.EqualTo(projectId));
            Assert.That(evt.HoursWorked, Is.EqualTo(8));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void WorkLogUpdatedEvent_Properties_CanBeSet()
    {
        // Arrange
        var workLogId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new WorkLogUpdatedEvent
        {
            WorkLogId = workLogId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.WorkLogId, Is.EqualTo(workLogId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void PartOrderedEvent_Properties_CanBeSet()
    {
        // Arrange
        var partId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PartOrderedEvent
        {
            PartId = partId,
            UserId = userId,
            ProjectId = projectId,
            Name = "Carburetor",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PartId, Is.EqualTo(partId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.ProjectId, Is.EqualTo(projectId));
            Assert.That(evt.Name, Is.EqualTo("Carburetor"));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void PartInstalledEvent_Properties_CanBeSet()
    {
        // Arrange
        var partId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PartInstalledEvent
        {
            PartId = partId,
            UserId = userId,
            ProjectId = projectId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PartId, Is.EqualTo(partId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.ProjectId, Is.EqualTo(projectId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void PhotoLoggedEvent_Properties_CanBeSet()
    {
        // Arrange
        var photoLogId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PhotoLoggedEvent
        {
            PhotoLogId = photoLogId,
            UserId = userId,
            ProjectId = projectId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PhotoLogId, Is.EqualTo(photoLogId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.ProjectId, Is.EqualTo(projectId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void PhotoLogUpdatedEvent_Properties_CanBeSet()
    {
        // Arrange
        var photoLogId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PhotoLogUpdatedEvent
        {
            PhotoLogId = photoLogId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PhotoLogId, Is.EqualTo(photoLogId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
