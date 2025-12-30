// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core.Tests;

public class MaterialAddedEventTests
{
    [Test]
    public void MaterialAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var materialId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var name = "2x4 Lumber";
        var timestamp = DateTime.UtcNow;

        // Act
        var materialEvent = new MaterialAddedEvent
        {
            MaterialId = materialId,
            ProjectId = projectId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(materialEvent.MaterialId, Is.EqualTo(materialId));
            Assert.That(materialEvent.ProjectId, Is.EqualTo(projectId));
            Assert.That(materialEvent.Name, Is.EqualTo(name));
            Assert.That(materialEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MaterialAddedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var materialEvent = new MaterialAddedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(materialEvent.MaterialId, Is.EqualTo(Guid.Empty));
            Assert.That(materialEvent.ProjectId, Is.EqualTo(Guid.Empty));
            Assert.That(materialEvent.Name, Is.EqualTo(string.Empty));
            Assert.That(materialEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MaterialAddedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var materialEvent = new MaterialAddedEvent
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Material"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(materialEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void MaterialAddedEvent_WithEmptyName_IsValid()
    {
        // Arrange & Act
        var materialEvent = new MaterialAddedEvent
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = ""
        };

        // Assert
        Assert.That(materialEvent.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void MaterialAddedEvent_IsImmutable()
    {
        // Arrange
        var materialId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var name = "Concrete";

        // Act
        var materialEvent = new MaterialAddedEvent
        {
            MaterialId = materialId,
            ProjectId = projectId,
            Name = name
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(materialEvent.MaterialId, Is.EqualTo(materialId));
            Assert.That(materialEvent.ProjectId, Is.EqualTo(projectId));
            Assert.That(materialEvent.Name, Is.EqualTo(name));
        });
    }

    [Test]
    public void MaterialAddedEvent_EqualityByValue()
    {
        // Arrange
        var materialId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var name = "Steel Beams";
        var timestamp = DateTime.UtcNow;

        var event1 = new MaterialAddedEvent
        {
            MaterialId = materialId,
            ProjectId = projectId,
            Name = name,
            Timestamp = timestamp
        };

        var event2 = new MaterialAddedEvent
        {
            MaterialId = materialId,
            ProjectId = projectId,
            Name = name,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void MaterialAddedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new MaterialAddedEvent
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Material 1"
        };

        var event2 = new MaterialAddedEvent
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Material 2"
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void MaterialAddedEvent_WithLongName_IsValid()
    {
        // Arrange
        var longName = new string('A', 500);

        // Act
        var materialEvent = new MaterialAddedEvent
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = longName
        };

        // Assert
        Assert.That(materialEvent.Name, Is.EqualTo(longName));
    }
}
