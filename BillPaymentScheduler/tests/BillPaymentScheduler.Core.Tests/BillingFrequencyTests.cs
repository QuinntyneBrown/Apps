// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core.Tests;

public class BillingFrequencyTests
{
    [Test]
    public void BillingFrequency_Weekly_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = BillingFrequency.Weekly;

        // Assert
        Assert.That((int)frequency, Is.EqualTo(0));
    }

    [Test]
    public void BillingFrequency_BiWeekly_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = BillingFrequency.BiWeekly;

        // Assert
        Assert.That((int)frequency, Is.EqualTo(1));
    }

    [Test]
    public void BillingFrequency_Monthly_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = BillingFrequency.Monthly;

        // Assert
        Assert.That((int)frequency, Is.EqualTo(2));
    }

    [Test]
    public void BillingFrequency_Quarterly_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = BillingFrequency.Quarterly;

        // Assert
        Assert.That((int)frequency, Is.EqualTo(3));
    }

    [Test]
    public void BillingFrequency_Annual_HasCorrectValue()
    {
        // Arrange & Act
        var frequency = BillingFrequency.Annual;

        // Assert
        Assert.That((int)frequency, Is.EqualTo(4));
    }

    [Test]
    public void BillingFrequency_CanBeAssignedToVariable()
    {
        // Arrange & Act
        BillingFrequency frequency = BillingFrequency.Monthly;

        // Assert
        Assert.That(frequency, Is.EqualTo(BillingFrequency.Monthly));
    }

    [Test]
    public void BillingFrequency_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var allValues = Enum.GetValues<BillingFrequency>();

        // Assert
        Assert.That(allValues, Has.Length.EqualTo(5));
        Assert.That(allValues, Contains.Item(BillingFrequency.Weekly));
        Assert.That(allValues, Contains.Item(BillingFrequency.BiWeekly));
        Assert.That(allValues, Contains.Item(BillingFrequency.Monthly));
        Assert.That(allValues, Contains.Item(BillingFrequency.Quarterly));
        Assert.That(allValues, Contains.Item(BillingFrequency.Annual));
    }
}
