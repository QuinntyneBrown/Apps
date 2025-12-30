// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Core.Tests;

public class BillingCycleTests
{
    [Test]
    public void BillingCycle_Weekly_HasValue0()
    {
        // Arrange & Act
        var cycle = BillingCycle.Weekly;

        // Assert
        Assert.That((int)cycle, Is.EqualTo(0));
    }

    [Test]
    public void BillingCycle_Monthly_HasValue1()
    {
        // Arrange & Act
        var cycle = BillingCycle.Monthly;

        // Assert
        Assert.That((int)cycle, Is.EqualTo(1));
    }

    [Test]
    public void BillingCycle_Quarterly_HasValue2()
    {
        // Arrange & Act
        var cycle = BillingCycle.Quarterly;

        // Assert
        Assert.That((int)cycle, Is.EqualTo(2));
    }

    [Test]
    public void BillingCycle_Annual_HasValue3()
    {
        // Arrange & Act
        var cycle = BillingCycle.Annual;

        // Assert
        Assert.That((int)cycle, Is.EqualTo(3));
    }

    [Test]
    public void BillingCycle_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var values = Enum.GetValues<BillingCycle>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(values, Has.Length.EqualTo(4));
            Assert.That(values, Contains.Item(BillingCycle.Weekly));
            Assert.That(values, Contains.Item(BillingCycle.Monthly));
            Assert.That(values, Contains.Item(BillingCycle.Quarterly));
            Assert.That(values, Contains.Item(BillingCycle.Annual));
        });
    }

    [Test]
    public void BillingCycle_CanCompareValues_InDefinedOrder()
    {
        // Arrange & Act & Assert
        Assert.That(BillingCycle.Weekly < BillingCycle.Monthly, Is.True);
        Assert.That(BillingCycle.Monthly < BillingCycle.Quarterly, Is.True);
        Assert.That(BillingCycle.Quarterly < BillingCycle.Annual, Is.True);
    }
}
