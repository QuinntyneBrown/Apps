// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Core.Tests;

public class SubscriptionStatusTests
{
    [Test]
    public void SubscriptionStatus_Active_HasValue0()
    {
        // Arrange & Act
        var status = SubscriptionStatus.Active;

        // Assert
        Assert.That((int)status, Is.EqualTo(0));
    }

    [Test]
    public void SubscriptionStatus_Paused_HasValue1()
    {
        // Arrange & Act
        var status = SubscriptionStatus.Paused;

        // Assert
        Assert.That((int)status, Is.EqualTo(1));
    }

    [Test]
    public void SubscriptionStatus_Cancelled_HasValue2()
    {
        // Arrange & Act
        var status = SubscriptionStatus.Cancelled;

        // Assert
        Assert.That((int)status, Is.EqualTo(2));
    }

    [Test]
    public void SubscriptionStatus_Pending_HasValue3()
    {
        // Arrange & Act
        var status = SubscriptionStatus.Pending;

        // Assert
        Assert.That((int)status, Is.EqualTo(3));
    }

    [Test]
    public void SubscriptionStatus_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var values = Enum.GetValues<SubscriptionStatus>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(values, Has.Length.EqualTo(4));
            Assert.That(values, Contains.Item(SubscriptionStatus.Active));
            Assert.That(values, Contains.Item(SubscriptionStatus.Paused));
            Assert.That(values, Contains.Item(SubscriptionStatus.Cancelled));
            Assert.That(values, Contains.Item(SubscriptionStatus.Pending));
        });
    }

    [Test]
    public void SubscriptionStatus_CanCompareValues_InDefinedOrder()
    {
        // Arrange & Act & Assert
        Assert.That(SubscriptionStatus.Active < SubscriptionStatus.Paused, Is.True);
        Assert.That(SubscriptionStatus.Paused < SubscriptionStatus.Cancelled, Is.True);
        Assert.That(SubscriptionStatus.Cancelled < SubscriptionStatus.Pending, Is.True);
    }
}
