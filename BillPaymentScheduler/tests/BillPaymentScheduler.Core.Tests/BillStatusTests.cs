// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core.Tests;

public class BillStatusTests
{
    [Test]
    public void BillStatus_Pending_HasCorrectValue()
    {
        // Arrange & Act
        var status = BillStatus.Pending;

        // Assert
        Assert.That((int)status, Is.EqualTo(0));
    }

    [Test]
    public void BillStatus_Paid_HasCorrectValue()
    {
        // Arrange & Act
        var status = BillStatus.Paid;

        // Assert
        Assert.That((int)status, Is.EqualTo(1));
    }

    [Test]
    public void BillStatus_Overdue_HasCorrectValue()
    {
        // Arrange & Act
        var status = BillStatus.Overdue;

        // Assert
        Assert.That((int)status, Is.EqualTo(2));
    }

    [Test]
    public void BillStatus_Cancelled_HasCorrectValue()
    {
        // Arrange & Act
        var status = BillStatus.Cancelled;

        // Assert
        Assert.That((int)status, Is.EqualTo(3));
    }

    [Test]
    public void BillStatus_CanBeAssignedToVariable()
    {
        // Arrange & Act
        BillStatus status = BillStatus.Paid;

        // Assert
        Assert.That(status, Is.EqualTo(BillStatus.Paid));
    }

    [Test]
    public void BillStatus_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var allValues = Enum.GetValues<BillStatus>();

        // Assert
        Assert.That(allValues, Has.Length.EqualTo(4));
        Assert.That(allValues, Contains.Item(BillStatus.Pending));
        Assert.That(allValues, Contains.Item(BillStatus.Paid));
        Assert.That(allValues, Contains.Item(BillStatus.Overdue));
        Assert.That(allValues, Contains.Item(BillStatus.Cancelled));
    }
}
