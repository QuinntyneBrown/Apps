// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BloodPressureMonitor.Core.Tests;

public class BloodPressureCategoryTests
{
    [Test]
    public void BloodPressureCategory_Normal_HasCorrectValue()
    {
        // Arrange & Act
        var category = BloodPressureCategory.Normal;

        // Assert
        Assert.That((int)category, Is.EqualTo(0));
    }

    [Test]
    public void BloodPressureCategory_Elevated_HasCorrectValue()
    {
        // Arrange & Act
        var category = BloodPressureCategory.Elevated;

        // Assert
        Assert.That((int)category, Is.EqualTo(1));
    }

    [Test]
    public void BloodPressureCategory_HypertensionStage1_HasCorrectValue()
    {
        // Arrange & Act
        var category = BloodPressureCategory.HypertensionStage1;

        // Assert
        Assert.That((int)category, Is.EqualTo(2));
    }

    [Test]
    public void BloodPressureCategory_HypertensionStage2_HasCorrectValue()
    {
        // Arrange & Act
        var category = BloodPressureCategory.HypertensionStage2;

        // Assert
        Assert.That((int)category, Is.EqualTo(3));
    }

    [Test]
    public void BloodPressureCategory_HypertensiveCrisis_HasCorrectValue()
    {
        // Arrange & Act
        var category = BloodPressureCategory.HypertensiveCrisis;

        // Assert
        Assert.That((int)category, Is.EqualTo(4));
    }

    [Test]
    public void BloodPressureCategory_CanBeAssignedToVariable()
    {
        // Arrange & Act
        BloodPressureCategory category = BloodPressureCategory.Normal;

        // Assert
        Assert.That(category, Is.EqualTo(BloodPressureCategory.Normal));
    }

    [Test]
    public void BloodPressureCategory_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var allValues = Enum.GetValues<BloodPressureCategory>();

        // Assert
        Assert.That(allValues, Has.Length.EqualTo(5));
        Assert.That(allValues, Contains.Item(BloodPressureCategory.Normal));
        Assert.That(allValues, Contains.Item(BloodPressureCategory.Elevated));
        Assert.That(allValues, Contains.Item(BloodPressureCategory.HypertensionStage1));
        Assert.That(allValues, Contains.Item(BloodPressureCategory.HypertensionStage2));
        Assert.That(allValues, Contains.Item(BloodPressureCategory.HypertensiveCrisis));
    }
}
