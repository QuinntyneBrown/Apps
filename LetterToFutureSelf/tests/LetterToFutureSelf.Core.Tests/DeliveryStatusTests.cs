// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Core.Tests;

public class DeliveryStatusTests
{
    [Test]
    public void DeliveryStatus_Pending_HasCorrectValue()
    {
        // Arrange & Act
        var status = DeliveryStatus.Pending;

        // Assert
        Assert.That((int)status, Is.EqualTo(0));
    }

    [Test]
    public void DeliveryStatus_Delivered_HasCorrectValue()
    {
        // Arrange & Act
        var status = DeliveryStatus.Delivered;

        // Assert
        Assert.That((int)status, Is.EqualTo(1));
    }

    [Test]
    public void DeliveryStatus_Cancelled_HasCorrectValue()
    {
        // Arrange & Act
        var status = DeliveryStatus.Cancelled;

        // Assert
        Assert.That((int)status, Is.EqualTo(2));
    }

    [Test]
    public void DeliveryStatus_Failed_HasCorrectValue()
    {
        // Arrange & Act
        var status = DeliveryStatus.Failed;

        // Assert
        Assert.That((int)status, Is.EqualTo(3));
    }

    [Test]
    public void DeliveryStatus_CanBeAssignedToLetter()
    {
        // Arrange
        var letter = new Letter();

        // Act
        letter.DeliveryStatus = DeliveryStatus.Delivered;

        // Assert
        Assert.That(letter.DeliveryStatus, Is.EqualTo(DeliveryStatus.Delivered));
    }

    [Test]
    public void DeliveryStatus_AllValuesCanBeAssigned()
    {
        // Arrange
        var letter = new Letter();
        var allStatuses = new[]
        {
            DeliveryStatus.Pending,
            DeliveryStatus.Delivered,
            DeliveryStatus.Cancelled,
            DeliveryStatus.Failed
        };

        // Act & Assert
        foreach (var status in allStatuses)
        {
            letter.DeliveryStatus = status;
            Assert.That(letter.DeliveryStatus, Is.EqualTo(status));
        }
    }
}
