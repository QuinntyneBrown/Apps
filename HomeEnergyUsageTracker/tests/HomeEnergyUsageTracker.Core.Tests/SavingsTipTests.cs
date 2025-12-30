// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeEnergyUsageTracker.Core.Tests;

public class SavingsTipTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesSavingsTip()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        var title = "Turn off lights when leaving room";
        var description = "This simple habit can save energy";
        var createdAt = DateTime.UtcNow;

        // Act
        var savingsTip = new SavingsTip
        {
            SavingsTipId = savingsTipId,
            Title = title,
            Description = description,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(savingsTip.SavingsTipId, Is.EqualTo(savingsTipId));
            Assert.That(savingsTip.Title, Is.EqualTo(title));
            Assert.That(savingsTip.Description, Is.EqualTo(description));
            Assert.That(savingsTip.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void SavingsTip_DefaultValues_AreSetCorrectly()
    {
        // Act
        var savingsTip = new SavingsTip();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(savingsTip.Title, Is.EqualTo(string.Empty));
            Assert.That(savingsTip.Description, Is.Null);
            Assert.That(savingsTip.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void SavingsTip_WithoutDescription_IsValid()
    {
        // Arrange & Act
        var savingsTip = new SavingsTip
        {
            SavingsTipId = Guid.NewGuid(),
            Title = "Energy saving tip"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(savingsTip.Title, Is.Not.Empty);
            Assert.That(savingsTip.Description, Is.Null);
        });
    }

    [Test]
    public void SavingsTip_WithDescription_IsValid()
    {
        // Arrange
        var description = "Detailed description of the energy saving tip";

        // Act
        var savingsTip = new SavingsTip
        {
            SavingsTipId = Guid.NewGuid(),
            Title = "Energy saving tip",
            Description = description
        };

        // Assert
        Assert.That(savingsTip.Description, Is.EqualTo(description));
    }

    [Test]
    public void SavingsTip_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var savingsTip = new SavingsTip
        {
            SavingsTipId = Guid.NewGuid(),
            Title = "Test Tip"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(savingsTip.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void SavingsTip_CanUpdateTitle()
    {
        // Arrange
        var savingsTip = new SavingsTip
        {
            SavingsTipId = Guid.NewGuid(),
            Title = "Original Title"
        };
        var newTitle = "Updated Title";

        // Act
        savingsTip.Title = newTitle;

        // Assert
        Assert.That(savingsTip.Title, Is.EqualTo(newTitle));
    }

    [Test]
    public void SavingsTip_CanUpdateDescription()
    {
        // Arrange
        var savingsTip = new SavingsTip
        {
            SavingsTipId = Guid.NewGuid(),
            Title = "Test Tip",
            Description = "Original description"
        };
        var newDescription = "Updated description";

        // Act
        savingsTip.Description = newDescription;

        // Assert
        Assert.That(savingsTip.Description, Is.EqualTo(newDescription));
    }

    [Test]
    public void SavingsTip_EmptyTitle_CanBeSet()
    {
        // Arrange & Act
        var savingsTip = new SavingsTip
        {
            SavingsTipId = Guid.NewGuid(),
            Title = ""
        };

        // Assert
        Assert.That(savingsTip.Title, Is.EqualTo(string.Empty));
    }

    [Test]
    public void SavingsTip_LongTitle_CanBeSet()
    {
        // Arrange
        var longTitle = new string('A', 500);

        // Act
        var savingsTip = new SavingsTip
        {
            SavingsTipId = Guid.NewGuid(),
            Title = longTitle
        };

        // Assert
        Assert.That(savingsTip.Title, Is.EqualTo(longTitle));
    }

    [Test]
    public void SavingsTip_AllProperties_CanBeSet()
    {
        // Arrange
        var savingsTipId = Guid.NewGuid();
        var title = "Complete Energy Saving Tip";
        var description = "This is a comprehensive description";
        var createdAt = DateTime.UtcNow.AddDays(-5);

        // Act
        var savingsTip = new SavingsTip
        {
            SavingsTipId = savingsTipId,
            Title = title,
            Description = description,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(savingsTip.SavingsTipId, Is.EqualTo(savingsTipId));
            Assert.That(savingsTip.Title, Is.EqualTo(title));
            Assert.That(savingsTip.Description, Is.EqualTo(description));
            Assert.That(savingsTip.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
