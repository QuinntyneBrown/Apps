// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ClassicCarRestorationLog.Core.Tests;

public class PartTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var part = new Part();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(part.Name, Is.EqualTo(string.Empty));
            Assert.That(part.PartNumber, Is.Null);
            Assert.That(part.Supplier, Is.Null);
            Assert.That(part.Cost, Is.Null);
            Assert.That(part.OrderedDate, Is.Null);
            Assert.That(part.ReceivedDate, Is.Null);
            Assert.That(part.IsInstalled, Is.False);
            Assert.That(part.Notes, Is.Null);
            Assert.That(part.Project, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var partId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var orderedDate = DateTime.UtcNow.AddDays(-30);
        var receivedDate = DateTime.UtcNow.AddDays(-15);

        // Act
        var part = new Part
        {
            PartId = partId,
            UserId = userId,
            ProjectId = projectId,
            Name = "Carburetor",
            PartNumber = "ABC-123",
            Supplier = "Classic Parts Inc.",
            Cost = 450.50m,
            OrderedDate = orderedDate,
            ReceivedDate = receivedDate,
            IsInstalled = true,
            Notes = "Original equipment manufacturer"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(part.PartId, Is.EqualTo(partId));
            Assert.That(part.UserId, Is.EqualTo(userId));
            Assert.That(part.ProjectId, Is.EqualTo(projectId));
            Assert.That(part.Name, Is.EqualTo("Carburetor"));
            Assert.That(part.PartNumber, Is.EqualTo("ABC-123"));
            Assert.That(part.Supplier, Is.EqualTo("Classic Parts Inc."));
            Assert.That(part.Cost, Is.EqualTo(450.50m));
            Assert.That(part.OrderedDate, Is.EqualTo(orderedDate));
            Assert.That(part.ReceivedDate, Is.EqualTo(receivedDate));
            Assert.That(part.IsInstalled, Is.True);
            Assert.That(part.Notes, Is.EqualTo("Original equipment manufacturer"));
        });
    }

    [Test]
    public void IsInstalled_DefaultsToFalse()
    {
        // Arrange & Act
        var part = new Part();

        // Assert
        Assert.That(part.IsInstalled, Is.False);
    }

    [Test]
    public void IsInstalled_CanBeSetToTrue()
    {
        // Arrange
        var part = new Part();

        // Act
        part.IsInstalled = true;

        // Assert
        Assert.That(part.IsInstalled, Is.True);
    }

    [Test]
    public void Cost_CanBeZero()
    {
        // Arrange & Act
        var part = new Part
        {
            Cost = 0m
        };

        // Assert
        Assert.That(part.Cost, Is.EqualTo(0m));
    }

    [Test]
    public void Cost_CanBeNegative()
    {
        // Arrange & Act
        var part = new Part
        {
            Cost = -10m
        };

        // Assert
        Assert.That(part.Cost, Is.EqualTo(-10m));
    }

    [Test]
    public void Project_CanBeAssigned()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            CarMake = "Chevrolet",
            CarModel = "Corvette"
        };
        var part = new Part();

        // Act
        part.Project = project;

        // Assert
        Assert.That(part.Project, Is.EqualTo(project));
    }

    [Test]
    public void ReceivedDate_CanBeSetBeforeOrderedDate()
    {
        // Arrange & Act
        var part = new Part
        {
            OrderedDate = DateTime.UtcNow,
            ReceivedDate = DateTime.UtcNow.AddDays(-1)
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(part.OrderedDate, Is.Not.Null);
            Assert.That(part.ReceivedDate, Is.Not.Null);
            Assert.That(part.ReceivedDate, Is.LessThan(part.OrderedDate));
        });
    }
}
