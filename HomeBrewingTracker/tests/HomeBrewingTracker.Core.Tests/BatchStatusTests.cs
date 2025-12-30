// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core.Tests;

public class BatchStatusTests
{
    [Test]
    public void BatchStatus_Planned_HasCorrectValue()
    {
        // Arrange & Act
        var status = BatchStatus.Planned;

        // Assert
        Assert.That(status, Is.EqualTo(BatchStatus.Planned));
        Assert.That((int)status, Is.EqualTo(0));
    }

    [Test]
    public void BatchStatus_Fermenting_HasCorrectValue()
    {
        // Arrange & Act
        var status = BatchStatus.Fermenting;

        // Assert
        Assert.That(status, Is.EqualTo(BatchStatus.Fermenting));
        Assert.That((int)status, Is.EqualTo(1));
    }

    [Test]
    public void BatchStatus_Bottled_HasCorrectValue()
    {
        // Arrange & Act
        var status = BatchStatus.Bottled;

        // Assert
        Assert.That(status, Is.EqualTo(BatchStatus.Bottled));
        Assert.That((int)status, Is.EqualTo(2));
    }

    [Test]
    public void BatchStatus_Conditioning_HasCorrectValue()
    {
        // Arrange & Act
        var status = BatchStatus.Conditioning;

        // Assert
        Assert.That(status, Is.EqualTo(BatchStatus.Conditioning));
        Assert.That((int)status, Is.EqualTo(3));
    }

    [Test]
    public void BatchStatus_Completed_HasCorrectValue()
    {
        // Arrange & Act
        var status = BatchStatus.Completed;

        // Assert
        Assert.That(status, Is.EqualTo(BatchStatus.Completed));
        Assert.That((int)status, Is.EqualTo(4));
    }

    [Test]
    public void BatchStatus_Failed_HasCorrectValue()
    {
        // Arrange & Act
        var status = BatchStatus.Failed;

        // Assert
        Assert.That(status, Is.EqualTo(BatchStatus.Failed));
        Assert.That((int)status, Is.EqualTo(5));
    }

    [Test]
    public void BatchStatus_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var statuses = new[]
        {
            BatchStatus.Planned,
            BatchStatus.Fermenting,
            BatchStatus.Bottled,
            BatchStatus.Conditioning,
            BatchStatus.Completed,
            BatchStatus.Failed
        };

        // Assert
        Assert.That(statuses, Has.Length.EqualTo(6));
    }

    [Test]
    public void BatchStatus_CanBeCompared()
    {
        // Arrange
        var status1 = BatchStatus.Fermenting;
        var status2 = BatchStatus.Fermenting;
        var status3 = BatchStatus.Bottled;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(status1, Is.EqualTo(status2));
            Assert.That(status1, Is.Not.EqualTo(status3));
        });
    }
}
